﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.MVVM.Model
{
    class CryptoСurrenciesList
    {
        public List<CryptoCurrency>? data { get; set; }
        public long timestamp { get; set; }
    }
}
