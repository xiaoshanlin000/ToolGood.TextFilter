﻿/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System.Collections.Generic;

namespace ToolGood.TextFilter.Common.Datas.Results
{
    public class IllegalWordsFindAllResult
    {
        public IllegalWordsRiskLevel RiskLevel;

        public string Code;

        public double? SentimentScore;

        /// <summary>
        /// 建议屏蔽 敏感词
        /// </summary>
        public List<SingleWordsResult> RejectSingleItems;

        /// <summary>
        /// 建议屏蔽 多组敏感词
        /// </summary>
        public List<MultiWordsResult> RejectMultiItems;

        /// <summary>
        /// 建议复审 敏感词
        /// </summary>
        public List<SingleWordsResult> ReviewSingleItems;

        /// <summary>
        /// 建议复审 多组敏感词
        /// </summary>
        public List<MultiWordsResult> ReviewMultiItems;

        /// <summary>
        /// 联系方式
        /// </summary>
        public List<ContactResult> ContactItems;

        public IllegalWordsFindAllResult()
        {
            RiskLevel = IllegalWordsRiskLevel.Pass;
            RejectSingleItems = new List<SingleWordsResult>();
            RejectMultiItems = new List<MultiWordsResult>();
            ReviewSingleItems = new List<SingleWordsResult>();
            ReviewMultiItems = new List<MultiWordsResult>();
            ContactItems = new List<ContactResult>();
        }

    }


}
