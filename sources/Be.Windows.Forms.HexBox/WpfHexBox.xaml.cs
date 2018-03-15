using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Be.Windows.Forms
{
    public partial class WpfHexBox : UserControl
    {
        /// <summary>
        /// Initialise HexBox.
        /// </summary>
        public WpfHexBox()
        {
            InitializeComponent();
            ((Be.Windows.Forms.HexBox)HexBox).SelectionStartChanged += WpfHexBox_SelectionStartChanged;
        }

        private void WpfHexBox_SelectionStartChanged(object sender, EventArgs e)
        {
            OnSelectionStartChanged?.Invoke(sender, e);
        }

        /// <summary>
        /// Byte Provider Interface.
        /// </summary>
        public IByteProvider ByteProvider
        {
            get { return (IByteProvider)GetValue(ByteProviderProperty); }
            set { SetValue(ByteProviderProperty, value); ClearHighlights(); }
        }

        /// <summary>
        /// Start offset of selection
        /// </summary>
        public long SelectionStart
        {
            get { return ((Be.Windows.Forms.HexBox)HexBox).SelectionStart;  }
        }

        /// <summary>
        /// Event handler on selection change.
        /// </summary>
        public event EventHandler OnSelectionStartChanged;

        /// <summary>
        /// Select bytes from [start] byte, for [length] bytes.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        public void Select(long start, long length)
        {
            ((Be.Windows.Forms.HexBox)HexBox).Select(start, length);
        }

        /// <summary>
        /// Highlights background of bytes between [startPos] for [length] bytes with [color].
        /// </summary>
        /// <param name="startPos">Start byte index.</param>
        /// <param name="length">How many bytes to highlight.</param>
        /// <param name="textColor">Colour for text.</param>
        /// <param name="bgColor">Colour to paint background.</param>
        /// <param name="clear">Clear highlighting for this position.</param>
        public void HighlightBytes(long startPos, long length, System.Drawing.Color textColor, System.Drawing.Color bgColor, bool clear = false)
        {
            ((Be.Windows.Forms.HexBox)HexBox).HighlightBytes(startPos, startPos + length, textColor, bgColor, clear);
        }

        /// <summary>
        /// Clears all highlights besides selection.
        /// </summary>
        /// <param name="refresh">Force redraw.</param>
        public void ClearHighlights(bool refresh = false)
        {
            ((Be.Windows.Forms.HexBox)HexBox).ClearHighlights();
        }

        /// <summary>
        /// Byte Provider.
        /// </summary>
        public static readonly DependencyProperty ByteProviderProperty =
            DependencyProperty.Register("ByteProvider", typeof(IByteProvider),
                typeof(WpfHexBox), new UIPropertyMetadata(null, ProviderChanged));

        private static void ProviderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((WpfHexBox)d).HexBox.ByteProvider = (IByteProvider)e.NewValue;
        }

    }
}