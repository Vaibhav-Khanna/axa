using System;
using System.IO;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using Xamarin.Forms;


namespace AXA.PageModels
{
    public class PdfViewerPageModel : BasePageModel
    {
        [PropertyChanged.DoNotNotify]
        Stream stream = new MemoryStream();

        [PropertyChanged.DoNotNotify]
        public Stream PdfDocumentStream { get { return stream; } set { stream = value; RaisePropertyChanged(); } }

        public Command BackCommand => new Command(async() =>
        {
            await CoreMethods.PopPageModel(false,false);
        });

		public override void Init(object initData)
		{
            base.Init(initData);

            if(initData is byte[])
            {
                PdfDocumentStream = new MemoryStream(initData as byte[]);
            }
            else if(initData is Tuple<byte[],int>)
            {
                PdfLoadedDocument loadedDocument = new PdfLoadedDocument(((Tuple<byte[], int>)initData).Item1);

                PdfDocument document = new PdfDocument();

                int startIndex = 0;

                int endIndex = ((Tuple<byte[], int>)initData).Item2 - 1;

                endIndex = Math.Min(loadedDocument.Pages.Count, endIndex);

                //Import all the pages to the new PDF document.
                document.ImportPageRange(loadedDocument, startIndex, endIndex);
                           
                var _stream = new MemoryStream();

                document.Save(_stream);
                              
                loadedDocument.Close();
                document.Close();

                PdfDocumentStream = _stream;
            }
		}

		protected override void ViewIsDisappearing(object sender, EventArgs e)
		{
            base.ViewIsDisappearing(sender, e);
		}
	}
}
