using EllieMae.Encompass.Automation;
using EllieMae.Encompass.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscrowCushionSetter
{
    [Plugin]
    public class SetCushion
    {
        private List<string> cushionFields = new List<string>()
        {
            "HUD30",
            "HUD31",
        };

        public SetCushion()
        {
            EncompassApplication.LoanOpened += EncompassApplication_LoanOpened;
            EncompassApplication.LoanClosing += EncompassApplication_LoanClosing;
        }

        private void EncompassApplication_LoanOpened(object sender, EventArgs e)
        {
            EncompassApplication.CurrentLoan.FieldChange += CurrentLoan_FieldChange;

            // if this loan has just been created, set cushion fields
            SetCushionFields();
        }

        private void CurrentLoan_FieldChange(object source, EllieMae.Encompass.BusinessObjects.Loans.FieldChangeEventArgs e)
        {
            // if cushion fields are ever changed, revert to the default cushion values
            if (cushionFields.Contains(e.FieldID))
            {
                SetCushionFields();
            }
        }

        private void EncompassApplication_LoanClosing(object sender, EventArgs e)
        {
            EncompassApplication.CurrentLoan.FieldChange -= CurrentLoan_FieldChange;
        }

        private void SetCushionFields()
        {
            foreach (string fieldId in cushionFields)
            {
                EncompassApplication.CurrentLoan.Fields[fieldId].Value = 2;
            }
        }
    }
}
