using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Siger;

namespace UMLShapes
{
    public class Package : CustomGesture
    {
        /*-----------Constructor With No Argument---------------*/ 
        public Package()
            : this(null)
        { }
        /*------------------------------------------------------*/

        /*-----------Constructoe with StrokeInfo Argument----------*/
        public Package(StrokeInfo si)
            : base(si)
        {
            Name = "Package";
        }
        /*---------------------------------------------------------*/

        /*-------------------------------Recognize Method which defines the condition for Package Shape Recognition------------------------*/ 
        protected override bool Recognize()
        {
            return (
                StrokeInfo.StrokeStatistics.StopPoints >= 4
                && StrokeInfo.StrokeStatistics.Square > 0.9
                && (StrokeInfo.IsMatch(Vectors.StartTick + Vectors.Downs + Vectors.Rights + Vectors.Ups
                                        + Vectors.Lefts + Vectors.Ups + Vectors.Rights + Vectors.Downs + Vectors.EndTick, 0, false, false)
                 || StrokeInfo.IsMatch(Vectors.StartTick + Vectors.Rights + Vectors.Downs + Vectors.Lefts
                                        + Vectors.Ups + Vectors.Rights + Vectors.Downs + Vectors.EndTick, 0, false, false)));
        }
        /*----------------------------------------------------------------------------------------------------------------------------------*/

    }
}
