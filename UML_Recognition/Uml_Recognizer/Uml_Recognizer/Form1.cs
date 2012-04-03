using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers;
using System.IO;
using Microsoft.Ink;
using Siger;
using UMLShapes;

namespace Uml_Recognizer
{
    public partial class Form1 : Form
    {
        private InkPicture drawingArea;
        private SigerRecognizer customReco;
        private System.Timers.Timer timeToDraw;
        private bool firstInkStroke;
        private int collectionCounter; //Total No of Ink Strokes Collections
        private RecognizeUseCaseDiagram useCaseDiagram;

        private string umlDiagram;

        public Form1()
        {
            InitializeComponent();

            /*-------------------------Create Drawing Area----------------------------*/ 
            drawingArea = new InkPicture();
            drawingArea.Dock = DockStyle.Fill;
            drawingArea.BorderStyle = BorderStyle.Fixed3D;
            drawingArea.BackColor = System.Drawing.Color.White;
            drawingArea.Name = "DrawingArea";
            panel1.Controls.Add(drawingArea);
            drawingArea.InkEnabled = true;
            /*------------------------------------------------------------------------*/
    
            /*--------Define Event Handler for Ink Stroke Event on Drawing Area-------*/ 
            drawingArea.Stroke += new InkCollectorStrokeEventHandler(drawingArea_Stroke);
            /*------------------------------------------------------------------------*/

            /*---------------------------Building a Recognizer List--------------------------*/
            customReco = new SigerRecognizer();
            customReco.RecognizerList.Add(new Triangle());
            customReco.RecognizerList.Add(new Package());
            /*-------------------------------------------------------------------------------*/

            /*--------------------------Object of Timer Class-----------------------------*/
            timeToDraw = new System.Timers.Timer(9000);
            timeToDraw.AutoReset = false;
            timeToDraw.Elapsed += new ElapsedEventHandler(timeToDraw_Elapsed);
            /*----------------------------------------------------------------------------*/

            firstInkStroke = true;
            collectionCounter = 1;

        }

        /*-------------------------Event Handler for Form1 Load Event-----------------------------*/
        private void Form1_Load(object sender, EventArgs e)
        {
            ChooseDiagramDialogBox dialogBox = new ChooseDiagramDialogBox(this);
            if (dialogBox.ShowDialog() == DialogResult.OK)
                umlDiagram = dialogBox.diagram;

            if (umlDiagram == "Use Case Diagram")
                useCaseDiagram = new RecognizeUseCaseDiagram();
        }
        /*---------------------------------------------------------------------------------------*/

        /*---------------------Implementation of Timer Elapsed Event Handler------------------*/
        void timeToDraw_Elapsed(object sender, ElapsedEventArgs e)
        {
            timeToDraw.Enabled = false;
            firstInkStroke = true;
            //MessageBox.Show(drawingArea.Ink.CustomStrokes[getCollectionName()].Count.ToString());
            RecognizeStrokesCollection();
            incrementCollectionCounter();

            
        }
        /*------------------------------------------------------------------------------------*/


        /*----------------------Implementation of Ink Stoke Event Handler----------------------*/
        void drawingArea_Stroke(object sender, InkCollectorStrokeEventArgs e)
        {
            //RecognizeStroke(drawingArea);
            if (firstInkStroke)
            {
                firstInkStroke = false;
                Strokes tmpStrokes = drawingArea.Ink.CreateStrokes();
                string collectionName = "Collection" + collectionCounter;

                drawingArea.Ink.CustomStrokes.Add(collectionName, tmpStrokes);
                drawingArea.Ink.CustomStrokes[collectionName].Add(e.Stroke);

                timeToDraw.Enabled = true;
            }
            else
            {
                drawingArea.Ink.CustomStrokes[getCollectionName()].Add(e.Stroke);
            }

        }
        /*-------------------------------------------------------------------------------------*/

        /*--------------------------Recognize the Collection of Ink Strokes----------------------------*/
        private void RecognizeStrokesCollection()
        {
            if (umlDiagram == "Use Case Diagram")
            {
                Strokes inkStrokes = drawingArea.Ink.CustomStrokes[getCollectionName()];
                switch (inkStrokes.Count)
                {
                    case 1:
                        if (useCaseDiagram.RecognizeUseCase(inkStrokes))
                            MessageBox.Show("Use Case Shape");
                        else
                            MessageBox.Show("No Gesture");
                    break;
                    
                    case 2:
                        if (useCaseDiagram.recognizePackage(drawingArea.Ink.CustomStrokes[getCollectionName()]))
                             MessageBox.Show("Package");
                        else
                            MessageBox.Show("No Gesture");
                    break;
                
                    case 4: case 5: case 6: case 7: case 8: case 9:
                        if (useCaseDiagram.RecognizeActor(drawingArea.Ink.CustomStrokes[getCollectionName()]))
                            MessageBox.Show("Actor");
                        else
                            MessageBox.Show("No Gesture");
                    break;
                    
                    default:
                            MessageBox.Show("No Gesture");
                    break;
                }

            }

        }
        /*----------------------------------------------------------------------------------------------*/

        /*------------------------Recognize the Gesture for Given Ink Stroke-------------------*/
        private void RecognizeStroke(InkPicture myDrawingArea)
        {
            Stroke stroke = myDrawingArea.Ink.Strokes[0];
            StrokeInfo sInfo = new StrokeInfo(stroke);
            CustomGesture[] gesture = customReco.Recognize(stroke);
            MessageBox.Show(myDrawingArea.Ink.Strokes.Count.ToString());
            try
            {
                MessageBox.Show(gesture[0].Name.ToString());
            }
            catch
            {
                MessageBox.Show("No Gesture");
            }
            

        }
        /*--------------------------------------------------------------------------------------*/


        /*-------------------Get the Name of Current Strokes Collection-------------------------*/
        private string getCollectionName()
        {
            return ("Collection" + collectionCounter);
        }
        /*--------------------------------------------------------------------------------------*/


        /*-----------------------Increment the Total No of Collections--------------------------*/
        private void incrementCollectionCounter()
        {
            collectionCounter++;
        }
        /*--------------------------------------------------------------------------------------*/


        /*-----------------------Decrement the Total No of Collections--------------------------*/
        private void decrementCollectionCounter()
        {
            if (collectionCounter > 0)
            {
                collectionCounter--;
            }
        }
        /*-------------------------------------------------------------------------------------*/


        /*----------------Event Handler for 'Exit' Menu Item Click Event------------------------*/
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        /*--------------------------------------------------------------------------------------*/


        /*-----------------------Event Handler for 'Erase' Menu Item Click Event-----------------*/ 
        private void eraseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawingArea.Ink.DeleteStrokes();
            drawingArea.Refresh();
        }
        /*--------------------------------------------------------------------------------------*/

        /*----------------------Event Handler for 'Save' Menu Item Click Event------------------*/
        private void sAVEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialogBox = new SaveFileDialog();

            if (saveDialogBox.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Byte[] b = drawingArea.Ink.Save(Microsoft.Ink.PersistenceFormat.InkSerializedFormat);
                FileStream fileStream = new FileStream(saveDialogBox.FileName, FileMode.Create);
                fileStream.Write(b, 0, b.Length);
                fileStream.Close();
            }
        }
        /*--------------------------------------------------------------------------------------*/

        /*------------------------Event Handler for 'Open' Menu Item Click Event----------------*/
        private void oPENToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialogBox = new OpenFileDialog();

            if (openDialogBox.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FileInfo fileInfo = new FileInfo(openDialogBox.FileName);
                Byte[] b = new Byte[fileInfo.Length];

                FileStream fileStream = new FileStream(openDialogBox.FileName, FileMode.Open);
                fileStream.Read(b, 0, b.Length);
                fileStream.Close();

                Ink ink = new Ink();
                ink.Load(b);

                drawingArea.InkEnabled = false;
                drawingArea.Ink = ink;
                drawingArea.InkEnabled = true;

                //RecognizeStroke(drawingArea);
            }
        }
        /*----------------------------------------------------------------------------------------*/

       
    }
}
