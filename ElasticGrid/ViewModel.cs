// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewModel.cs" company="urb31075">
// All Right Reserved  
// </copyright>
// <summary>
//   The view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ElasticGrid
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    /// <summary>
    /// The view model.
    /// </summary>
    public class ViewModel
    {
        /// <summary>
        /// The prepare for binding.
        /// </summary>
        /// <param name="controlPointConfig">
        /// The control point config.
        /// </param>
        /// <param name="engineeringSurveyProjectInfo">
        /// The engineering survey project info.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>

        /// <summary>
        /// The view column dictionary.
        /// </summary>
        public readonly Dictionary<int, ControlPointConfig> ColumnHeaderDictionary = new Dictionary<int, ViewModel.ControlPointConfig>
                 {
                     { 0, new ControlPointConfig("Name", "Наименование", Type.GetType("System.String")) },
                     { 1, new ControlPointConfig("Amount", "Кол-во", Type.GetType("System.Decimal")) },
                     { 2, new ControlPointConfig("Summa", "Сумма", Type.GetType("System.Decimal")) },
                     { 3, new ControlPointConfig("Comment", "Комментарий", Type.GetType("System.String")) }
                 };

        /// <summary>
        /// The prepare for binding.
        /// </summary>
        /// <param name="controlPointConfig">
        /// The control point config.
        /// </param>
        /// <param name="engineeringSurveyProjectInfo">
        /// The engineering survey project info.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable PrepareForBinding(Dictionary<int, ControlPointConfig> controlPointConfig, IReadOnlyList<TestData> engineeringSurveyProjectInfo)
        {
            if (engineeringSurveyProjectInfo == null)
            {
                return null;
            }

            var controlPointDataList = controlPointConfig.Select(viewColumn => new ControlPointData(viewColumn.Value.Name, viewColumn.Value.Header, viewColumn.Value.DataType)).ToList();
            controlPointDataList.ForEach(c => c.Value = new List<object>());
            foreach (var info in engineeringSurveyProjectInfo)
            {
                var count = 0;
                controlPointDataList[count++].Value.Add(info.Name);
                controlPointDataList[count++].Value.Add(info.Amount);
                controlPointDataList[count++].Value.Add(info.Summa);
                controlPointDataList[count++].Value.Add(info.Comment);
            }

            return ConvertToDataTable(controlPointDataList);
        }

        /// <summary>
        /// The control point data bind.
        /// </summary>
        /// <param name="controlPointDataList">
        /// The control point data list.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        private static DataTable ConvertToDataTable(IReadOnlyList<ControlPointData> controlPointDataList)
        {
            if (controlPointDataList == null)
            {
                return null;
            }

            var dt = new DataTable();
            foreach (var data in controlPointDataList)
            {
                dt.Columns.Add(new DataColumn(data.Name, data.DataType));
            }

            for (var i = 0; i < controlPointDataList[0].Value.Count; i++)
            {
                var dr = dt.NewRow();
                foreach (var controlPointData in controlPointDataList)
                {
                    dr[controlPointData.Name] = controlPointData.Value[i];
                }

                dt.Rows.Add(dr);
            }

            return dt;
        }

        /// <summary>
        /// The control point config.
        /// </summary>
        public class ControlPointConfig
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ControlPointConfig"/> class.
            /// </summary>
            /// <param name="name">
            /// The name.
            /// </param>
            /// <param name="header">
            /// The header.
            /// </param>
            /// <param name="dataType">
            /// The data type.
            /// </param>
            public ControlPointConfig(string name, string header, Type dataType)
            {
                this.Name = name;
                this.Header = header;
                this.DataType = dataType;
            }

            /// <summary>
            /// Gets the data type.
            /// </summary>
            public Type DataType { get; }

            /// <summary>
            /// Gets the header.
            /// </summary>
            public string Header { get; }

            /// <summary>
            /// Gets the name.
            /// </summary>
            public string Name { get; }
        }

        /// <summary>
        /// The control point data.
        /// </summary>
        public class ControlPointData : ControlPointConfig
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ControlPointData"/> class.
            /// </summary>
            /// <param name="name">
            /// The name.
            /// </param>
            /// <param name="header">
            /// The header.
            /// </param>
            /// <param name="dataType">
            /// The data type.
            /// </param>
            public ControlPointData(string name, string header, Type dataType)
                : base(name, header, dataType)
            {
            }

            /// <summary>
            /// Gets or sets the value.
            /// </summary>
            public List<object> Value { get; set; }
        }

        /// <summary>
        /// The test data.
        /// </summary>
        public class TestData
        {
            public string Name { get; set; }

            public decimal Amount { get; set; }

            public decimal Summa { get; set; }

            public string Comment { get; set; }
        }
    }
}