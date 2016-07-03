using System.Web.Mvc;

namespace CourseRegistrationSystem.Infrastructure
{
    // implements the Transaction property defined in NHibernate
    // a transaction in Db parlance allows multiple SQL statements (Unit of Work) to be executed
    // at a time, it keeps track of every action performed on the DB.
    public class TransactionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception == null)
                Database.Session.Transaction.Commit(); // if no error, it commit the statements
            else
                Database.Session.Transaction.Rollback(); // otherwise, it rolls back all actions done
        }

        // called when an action is about to be carried out
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Database.Session.BeginTransaction();
        }
    }
}
