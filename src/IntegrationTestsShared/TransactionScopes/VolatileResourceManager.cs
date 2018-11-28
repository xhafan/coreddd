using System;
using System.Transactions;

namespace IntegrationTestsShared.TransactionScopes
{
    // https://stackoverflow.com/questions/5265841/how-to-create-a-class-that-works-with-transactionscope
    // inspired by https://www.developer.com/net/net/article.php/3565196/SystemTransactions-Implement-Your-Own-Resource-Manager.htm
    public class VolatileResourceManager : IEnlistmentNotification
    {
        private int _memberValue;
        private int _oldMemberValue;
        public int MemberValue => _memberValue;

        public void SetMemberValue(int newMemberValue)
        {
            _oldMemberValue = _memberValue;
            _memberValue = newMemberValue;
        }

        public void EnlistIntoTransactionScope(TransactionScope transactionScope)
        {
            if (transactionScope == null) throw new Exception("transactionScope is null");
            var currentTransaction = Transaction.Current;
            if (currentTransaction == null) throw new Exception("There is no ambient transaction");

            currentTransaction.EnlistVolatile(this, EnlistmentOptions.None);
        }

        public void Commit(Enlistment enlistment)
        {
            // Clear out oldMemberValue
            _oldMemberValue = 0;
        }

        public void InDoubt(Enlistment enlistment)
        {
        }

        public void Prepare(PreparingEnlistment preparingEnlistment)
        {
            preparingEnlistment.Prepared();
        }

        public void Rollback(Enlistment enlistment)
        {
            // Restore previous state
            _memberValue = _oldMemberValue;
            _oldMemberValue = 0;
        }
    }
}