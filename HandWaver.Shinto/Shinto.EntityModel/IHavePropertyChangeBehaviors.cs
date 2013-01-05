using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HandWaver.Shinto.EntityModel
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHavePropertyChangeBehaviors
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool HasBehavior<T>() where T : IPropertyChangedBehavior;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newBehavior"></param>
        void AddBehavior(IPropertyChangedBehavior newBehavior);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void RemoveBehavior<T>() where T : IPropertyChangedBehavior;
    }
}
