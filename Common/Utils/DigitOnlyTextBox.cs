using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace Common.Utils
{
    public class DigitOnlyTextBox : TextBox
    {
        public DigitOnlyTextBox()
        {
            PreviewTextInput += NumberTextBoxPreviewInput;
        }

        private void NumberTextBoxPreviewInput(object sender, TextCompositionEventArgs textCompositionEventArgs)
        {
            if (!char.IsDigit(textCompositionEventArgs.Text.ToCharArray().First()))
            {
                textCompositionEventArgs.Handled = true;
            }
        }
    }
}
