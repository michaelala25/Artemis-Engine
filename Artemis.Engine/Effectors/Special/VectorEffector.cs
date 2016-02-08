﻿#region Using Statements

using Microsoft.Xna.Framework;

using System;

#endregion

namespace Artemis.Engine.Effectors.Special
{
    /// <summary>
    /// An effector that acts on Vector2 objects.
    /// </summary>
    public class VectorEffector : Effector<Vector2> 
    {

        #region Constructors

        public VectorEffector( string fieldName
                             , string effectorName
                             , Func<double, int, Vector2> func
                             , EffectorOperatorType opType = EffectorOperatorType.InPlace
                             , EffectorValueType valueType = EffectorValueType.RelativeToStart
                             , bool reusable = false )
            : base(fieldName, effectorName, func, opType, valueType, reusable) { }

        public VectorEffector( string fieldName
                             , string effectorName
                             , Func<double, int, Vector2> func
                             , Func<Vector2, Vector2, Vector2> op
                             , EffectorValueType valueType = EffectorValueType.RelativeToStart
                             , bool reusable = false )
            : base(fieldName, effectorName, func, op, valueType, reusable) { }

        public VectorEffector( string fieldName
                             , string effectorName
                             , Func<double, int, Vector2> func
                             , EffectorOperator<Vector2> op
                             , EffectorValueType valueType = EffectorValueType.RelativeToStart
                             , bool reusable = false )
            : base(fieldName, effectorName, func, op, valueType, reusable) { }

        #endregion

    }
}