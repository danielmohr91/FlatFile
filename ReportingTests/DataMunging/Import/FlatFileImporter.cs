﻿using System.Collections.Generic;
using FlatFile.FixedWidth.Implementation;
using FlatFile.FixedWidth.Interfaces;

namespace DataMunging.Reporting.Import
{
    public abstract class FlatFileImporter<T> where T : new()
    {
        private readonly IFixedWidthFileParser<T> parser;
        private ICollection<T> rows;

        protected FlatFileImporter(string fileName)
        {
            parser = new FixedWidthFileParser<T>(GetLayout(fileName), fileName);
        }

        public abstract IFlatFileLayoutDescriptor<T> GetLayout(string fileName);

        public ICollection<T> GetRows()
        {
            return rows ?? (rows = parser.ParseFile(true, true));
        }

        public ICollection<T> GetRows(ITestForSkip testForSkip)
        {
            return rows ?? (rows = parser.ParseFile(testForSkip, true, true));
        }
    }
}