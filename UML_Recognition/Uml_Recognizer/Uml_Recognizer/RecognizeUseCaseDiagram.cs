using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Ink;
using Siger;

namespace Uml_Recognizer
{
    class RecognizeUseCaseDiagram
    {
        const Double CLOSED_PROXIMITY = 500;

        public RecognizeUseCaseDiagram()
        {

        }

        public bool recognizePackage(Strokes inkStrokes)
        {
            if (inkStrokes.Count == 2)
            {
                StrokeInfo strokeInfo1 = new StrokeInfo(inkStrokes[0]);
                StrokeInfo strokeInfo2 = new StrokeInfo(inkStrokes[1]);
                if (strokeInfo1.StrokeStatistics.Square > 0.9 && strokeInfo1.StrokeStatistics.Diagonal < 0.09 &&
                    strokeInfo1.StrokeStatistics.StartEndProximity < CLOSED_PROXIMITY)
                {
                    if (strokeInfo2.StrokeStatistics.Square > 0.9 && strokeInfo2.StrokeStatistics.Diagonal < 0.09)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }

            }
            else
                return false;
        }

        public bool RecognizeActor(Strokes inkStrokes)
        {
            if (inkStrokes.Count >= 4)
            {
                StrokeInfo strokeInfo1 = new StrokeInfo(inkStrokes[0]);
                StrokeInfo strokeInfo2 = new StrokeInfo(inkStrokes[1]);

                if (strokeInfo1.StrokeStatistics.StartEndProximity < CLOSED_PROXIMITY &&
                   strokeInfo1.StrokeStatistics.Square < 0.85 && strokeInfo1.StrokeStatistics.Diagonal > 0.15)
                {
                    if (strokeInfo2.StrokeStatistics.StartEndProximity > CLOSED_PROXIMITY &&
                        strokeInfo2.StrokeStatistics.Square > 0.9 && strokeInfo2.StrokeStatistics.Diagonal < 0.2)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public bool RecognizeUseCase(Strokes inkStrokes)
        {
            if (inkStrokes.Count == 1)
            {
                StrokeInfo strokeInfo1 = new StrokeInfo(inkStrokes[0]);

                if (strokeInfo1.StrokeStatistics.StartEndProximity < CLOSED_PROXIMITY &&
                    strokeInfo1.StrokeStatistics.Square < 0.85 && strokeInfo1.StrokeStatistics.Diagonal > 0.13)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
    }
}
