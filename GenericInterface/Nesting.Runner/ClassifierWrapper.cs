using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Nesting.Core.Classes.Classification;
using Nesting.Core.Interfaces;

namespace Nesting.Runner
{
    /// <summary>
    /// This class wraps a classifier, protecting main class from unexpected classifier errors and invoking classification operations in a asynchronous way.
    /// </summary>
    public class ClassifierWrapper
    {
        //Code for async operation found at http://www.csharp-examples.net/create-asynchronous-method/

        private INestingClassifier classifier;

        private bool running = false;

        public bool IsBusy { get { return running; } }

        private DateTime? startTime { get; set; }
        private DateTime? endTime { get; set; }

        private readonly object lockObject = new object();

        public ClassifierWrapper(INestingClassifier classifier)
        {
            this.classifier = classifier;

        }

        #region Classification

        #region Async

        private delegate List<ClassificationResult> ClassifyAllDelegate();
        public event AsyncCompletedEventHandler ClassifyAllCompleted;
        protected virtual void OnClassifyAllCompleted(AsyncCompletedEventArgs e)
        {
            if (ClassifyAllCompleted != null)
                ClassifyAllCompleted(this, e);
        }
        public void ClassifyAllAsync()
        {
            ClassifyAllDelegate worker = new ClassifyAllDelegate(ClassifyAll);
            AsyncCallback completedCallback = new AsyncCallback(ClassificationCompletedAll);

            lock (lockObject)
            {
                if (running)
                    throw new InvalidOperationException("Classifier busy!");

                worker.BeginInvoke(completedCallback, AsyncOperationManager.CreateOperation(null));
                running = true;
            }
        }
        private void ClassificationCompletedAll(IAsyncResult ar)
        {
            // get the original worker delegate and the AsyncOperation instance
            ClassifyAllDelegate worker = (ClassifyAllDelegate)((AsyncResult)ar).AsyncDelegate;
            AsyncOperation async = (AsyncOperation)ar.AsyncState;

            // finish the asynchronous operation
            List<ClassificationResult> results = worker.EndInvoke(ar);

            // clear the running task flag
            lock (lockObject)
            {
                running = false;
            }

            // raise the completed event
            AsyncCompletedEventArgs completedArgs = new AsyncCompletedEventArgs(null, false, results);
            async.PostOperationCompleted(delegate(object e) { OnClassifyAllCompleted((AsyncCompletedEventArgs)e); }, completedArgs);
        }

        #endregion Async
        
        public List<ClassificationResult> ClassifyAll()
        {
            ClassifierInformation information = classifier.GetClassifierInformation();

            try
            {
                startTime = DateTime.UtcNow;

                List<ClassificationResult> temp = classifier.ClassifyAll();

                List<ClassificationResult> result = new List<ClassificationResult>();


                endTime = DateTime.UtcNow;


                foreach (ClassificationResult res in temp)
                {
                    res.TimeTaken = endTime - startTime;

                    float partsArea = res.Parts.Select(x => x.GetTotalArea()).Sum();

                    res.RemainingArea = res.WorkingArea.GetTotalArea() - partsArea;

                    result.Add(res);
                }

                return result.OrderBy(x=>x.RemainingArea).ToList();
            }
            catch (Exception ex)
            {
                ClassificationResult result = new ClassificationResult(information);
                result.Error = ex;
                result.HasError = true;
                endTime = DateTime.UtcNow;
                result.TimeTaken = endTime - startTime;
                return new List<ClassificationResult>() { result };
            }
        }
        
        #endregion Classification


    }
}
