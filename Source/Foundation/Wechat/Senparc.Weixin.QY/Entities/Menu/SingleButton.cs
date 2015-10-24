﻿/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：SingleButton.cs
    文件功能描述：所有单击按钮的基类
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.QY.Entities.Menu
{
    /// <summary>
    ///     所有单击按钮的基类（view，click等）
    /// </summary>
    public abstract class SingleButton : BaseButton, IBaseButton
    {
        public SingleButton(string theType)
        {
            type = theType;
        }

        /// <summary>
        ///     按钮类型（click或view）
        /// </summary>
        public string type { get; set; }
    }
}