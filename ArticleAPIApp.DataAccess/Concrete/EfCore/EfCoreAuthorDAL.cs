﻿using ArticleAPIApp.DataAccess.Abstract;
using ArticleAPIApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArticleAPIApp.DataAccess.Concrete.EfCore
{
    public class EfCoreAuthorDAL : EfCoreGenericRepository<Author, ArticleAppContext>, IAuthorDAL
    {
    }
}
