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
            char lastChar = textCompositionEventArgs.Text.ToCharArray().First();
            if (!char.IsDigit(lastChar) || lastChar == ' ')
            {
                textCompositionEventArgs.Handled = true;
            }
        }
    }
}
