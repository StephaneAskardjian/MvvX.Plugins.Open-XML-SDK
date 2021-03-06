﻿using DocumentFormat.OpenXml.Wordprocessing;
using MvvX.Plugins.OpenXMLSDK.Word;
using MvvX.Plugins.OpenXMLSDK.Word.Tables;
using System.Linq;
using System;

namespace MvvX.Plugins.OpenXMLSDK.Platform.Word.Tables
{
    public class PlatformTableProperties : PlatformOpenXmlElement, ITableProperties
    {
        private readonly TableProperties xmlElement;

        public PlatformTableProperties()
            : this(new TableProperties())
        {
        }

        public PlatformTableProperties(TableProperties tableProperties)
            : base(tableProperties)
        {
            this.xmlElement = tableProperties;
        }

        #region Interface
        
        private ITableStyle tableStyle;
        public ITableStyle TableStyle
        {
            get
            {
                if (tableStyle == null)
                    tableStyle = PlatformTableStyle.New(xmlElement);

                return tableStyle;
            }
        }

        private ITableBorders tableborder;
        public ITableBorders TableBorders
        {
            get
            {
                if (tableborder == null)
                    tableborder = PlatformTableBorders.New(xmlElement);

                return tableborder;
            }
        }

        private IShading shading;
        public IShading Shading
        {
            get
            {
                if (shading == null)
                    shading = PlatformShading.New(xmlElement);

                return shading;
            }
        }

        private ITableWidth tableWidth;
        public ITableWidth TableWidth
        {
            get
            {
                if (tableWidth == null)
                    tableWidth = PlatformTableWidth.New(xmlElement);

                return tableWidth;
            }
        }
        
        public OpenXMLSDK.Word.Tables.TableRowAlignmentValues? TableJustification
        {
            get
            {
                if (xmlElement.TableJustification == null || !xmlElement.TableJustification.Val.HasValue)
                    return null;
                else
                    return (OpenXMLSDK.Word.Tables.TableRowAlignmentValues)(int)xmlElement.TableJustification.Val.Value;
            }
            set
            {
                if (value == null)
                    xmlElement.TableJustification = null;
                else
                {
                    if (xmlElement.TableJustification == null)
                        xmlElement.TableJustification = new TableJustification();
                    xmlElement.TableJustification.Val = (DocumentFormat.OpenXml.Wordprocessing.TableRowAlignmentValues)(int)value;
                }
            }
        }

        private ITableLayout layout;
        public ITableLayout Layout
        {
            get
            {
                if (layout == null)
                    layout = PlatformTableLayout.New(xmlElement);

                return layout;
            }
        }

        #endregion

        #region Static helpers methods

        public static PlatformTableProperties New(Table table)
        {
            TableProperties xmlElement = null;
            if (table.Descendants<TableProperties>().Any())
                xmlElement = table.Descendants<TableProperties>().First();
            else
            {
                xmlElement = new TableProperties();
                table.Append(xmlElement);
            }
            return new PlatformTableProperties(xmlElement);
        }
    }

    #endregion
}
