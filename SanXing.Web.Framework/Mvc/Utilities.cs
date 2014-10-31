using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Linq;

namespace SanXing.Web.Framework.Mvc
{
    public class Utilities
    {
        public static string GetInnerMostException(Exception ex)
        {
            return ex.GetBaseException().Message;
        }


        #region Select
        public static IList<SelectListItem> GetSelectListData<T>(IEnumerable<T> entities,
            Func<T, object> funcToGetValue,
            Func<T, object> funcToGetText,
            bool addDefaultSelectItem = true,
            bool defaultValueisZero = false)
        {
            var eList = entities
                   .Select(x => new SelectListItem
                   {
                       Value = funcToGetValue(x).ToString(),
                       Text = funcToGetText(x).ToString()

                   }).ToList();

            if (addDefaultSelectItem)
                eList.Insert(0, new SelectListItem { Selected = true, Text = "请选择", Value = defaultValueisZero ? "0" : "" });

            return eList;
        }

        public static IList<SelectListItem> GetSelectListData<T>(IEnumerable<T> entities,
            Func<T, object> funcToGetValue,
            Func<T, object> funcToGetText,
            List<int> SeletdValues,
            bool addDefaultSelectItem = true)
        {
            var list = GetSelectListData(entities, funcToGetValue, funcToGetText, addDefaultSelectItem);

            foreach (var item in list)
            {
                if (SeletdValues.Contains(Convert.ToInt32(item.Value)))
                {
                    item.Selected = true;
                }
            }
            return list;
        }

        public static IList<SelectListItem> GetSelectListData<T>(IEnumerable<T> entities,
            Func<T, object> funcToGetValue,
            Func<T, object> funcToGetText,
            int value,
            bool addDefaultSelectItem = true,
            bool defaultValueisZero = false)
        {
            var list = GetSelectListData(entities, funcToGetValue, funcToGetText, addDefaultSelectItem, defaultValueisZero);

            foreach (var item in list)
            {
                if (item.Value != "")
                {

                    if (value == Convert.ToInt32(item.Value))
                    {
                        item.Selected = true;
                    }
                }
            }
            return list;
        }

        public static IList<SelectListItem> GetSelectListData<T>(IEnumerable<T> entities,
          Func<T, object> funcToGetValue,
          Func<T, object> funcToGetText,
          int? value,
          bool addDefaultSelectItem = true,
          bool defaultValueisZero = false)
        {
            if (value.HasValue)
            {
                return GetSelectListData(entities,
                 funcToGetValue,
                 funcToGetText,
                 value.Value,
                 addDefaultSelectItem,
                 defaultValueisZero);
            }
            else
            {
                return GetSelectListData(entities,
                   funcToGetValue,
                   funcToGetText,
                   addDefaultSelectItem,
                   defaultValueisZero);

            }
        }


        public static SelectList CreateSelectList<T>(IEnumerable<T> entities, Func<T, object> funcToGetValue, Func<T, object> funcToGetText, bool addDefaultSelectItem = true)
        {
            return new SelectList(GetSelectListData(entities, funcToGetValue, funcToGetText, addDefaultSelectItem), "Value", "Text");

        }

        #endregion



        public static int GetMonthDayCount(int year, int month)
        {
            var list = new Dictionary<int, int>();
            list.Add(1, 31);
            list.Add(2, 28);
            list.Add(3, 31);
            list.Add(4, 30);
            list.Add(5, 31);
            list.Add(6, 30);
            list.Add(7, 31);
            list.Add(8, 31);
            list.Add(9, 30);
            list.Add(10, 31);
            list.Add(11, 30);
            list.Add(12, 31);
            var day = list.First(x => x.Key == month).Value;
            if (year % 4 == 0)
            {
                day++;
            }
            return day;
        }
    }
}