using ArticleAPIApp.DataAccess.Abstract;
using ArticleAPIApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ArticleAPIApp.DataAccess.Concrete.Memory
{
    public class MemoryArticleDAL : IArticleDAL
    {
        public MemoryArticleDAL()
        {
            if (Articles == null)
            {
                Articles = new List<Article>();
            }
        }
        public List<Article> Articles { get; set; }

        public int Create(Article entity)
        {
            int oldCount = Articles.Count;
            Articles.Add(entity);
            if (Articles.Count > oldCount)
            {
                return 1;
            }
            return 0;
        }

        public int Delete(Article entity)
        {
            int oldCount = Articles.Count;
            Articles.Remove(entity);
            if (Articles.Count > oldCount)
            {
                return 1;
            }
            return 0;
        }

        public ICollection<Article> GetAllByFilterOrNotFiltered(Expression<Func<Article, bool>> filter = null)
        {
            //ICollection<Author> authors = new List<Author>()
            //{
            //    new Author() { AuthorId =1, IsActive = true, CreateDate = DateTime.Now, Name = "Ali", Surname = "Delice"  },
            //    //new Author() { AuthorId=2, IsActive = true, CreateDate = DateTime.Now, Name = "Mustafa", Surname = "Erdoğan"}
            //};

            //ICollection<Category> categories = new List<Category>()
            //{
            //    new Category(){ CategoryId = 1, Name = "Matematik Öğretimi", IsActive = true , CreateDate = DateTime.Now},
            //    new Category(){ CategoryId = 2, Name = "Hukuk", IsActive = true , CreateDate = DateTime.Now}
            //};

            if (Articles.Count <= 0)
            {
                Articles.Add(new Article()
                {
                    ArticleId = 1,
                    IsActive = true,
                    Title = "Ölçek Geliştirme Ve Uyarlama Çalışmalarının İncelenmesi",
                    Content = "Bu çalışmanın amacı Türkiye’de 2005-2014 yılları arasında hakemli dergilerde matematik eğitimi alanında yayınlanan ölçek geliştirme ve uyarlama çalışmalarının sahip olduğu örneklem büyüklüğü, madde sayısı, Cronbach Alpha katsayısı değerlerinin ve ölçek geliştirme ve uyarlama adımlarının karakteristik özelliklerinin incelenmesidir. Zengin veri elde etmeye odaklı nitel paradigmaya sahip olan bu araştırma yorumlayıcı yaklaşımı benimsemiştir. Özellikle matematik eğitiminde yapılan ölçek geliştirme ve uyarlama çalışmalarına odaklanması araştırmanın özel durum çalışması deseni ile yürütülmesine sebep olmuştur. İncelenen 54 derginin 24 tanesinde 35 ölçek geliştirme ve 18 ölçek uyarlama çalışması, Pearson korelasyon katsayısı testi ve Ölçek Geliştirme Formu, Ölçek İnceleme Formu kullanılarak kestirimsel ve betimsel istatistikler yardımıyla sunulmuştur. Araştırma sonucunda çalışmaların neredeyse tamamında Cronbach Alpha katsayılarının 0,80 değerinden fazla çıktığı, örneklem büyüklüğü ile madde sayısı arasında anlamlı bir ilişki bulunmadığı ve bazı araştırmalarda madde sayısına düşen uygulayıcı sayısının beşten az olduğu sonucuna ulaşılmıştır. Ayrıca ölçek geliştirme çalışmalarının %65,51’inde ölçek geliştirme adımlarının gerçekleştirildiği, ölçek uyarlama çalışmalarının %52,96’sında ölçek uyarlama adımlarının gerçekleştirildiği görülmüştür. Bununla birlikte ölçek geliştirme ve uyarlama çalışmalarında,  deneme uygulamanın ve geçerlik çalışmalarının yapılması gibi adımların gerçekleştirildiğine ilişkin bilgilerin raporlanmadığı ve dolayısıyla ve araştırmacıların ölçek geliştirme ve uyarlama çalışmalarına yeteri kadar ilgi göstermediği sonucuna ulaşılmıştır.",
                    CreateDate = DateTime.Now.Date,
                    Author = new Author() { AuthorId = 1, IsActive = true, CreateDate = DateTime.Now, Name = "Ali", Surname = "Delice" },
                    Category = new Category() { CategoryId = 1, Name = "Matematik Öğretimi", IsActive = true, CreateDate = DateTime.Now.Date }
                });

                Articles.Add(new Article()
                {
                    ArticleId = 2,
                    IsActive = true,
                    Title = "Ölçek Geliştirme Ve Uyarlama Çalışmalarının İncelenmesi",
                    Content = "Gerek demokrasi gerekse anayasal yönetim çağımızın önde gelen siyasî idealleri arasında yer almaktadır. “Anayasal demokrasi” terimi bu iki ideali birleştiren bir rejim modelini ifade etmektedir. Ancak bu terim aynı zamanda kendi içinde bir gerilimi de barındırmaktadır. Anayasacılıkla demokrasi arasındaki gerilimin en belirgin şekilde kendini gösterdiği alan “anayasa yargısı”dır. Anayasa yargısının demokratik meşruluğu sorunu son yıllarda Türkiye’de de tartışılmaktadır. Bu makalede Türkiye Anayasa Mahkemesi’yle ilgili asıl sorunun onun demokratik çoğunlukları anayasal yönetimin gerekleriyle sınırlamasından ziyade, ideolojik devlet iktidarı adına sınırlaması olduğu savunulmaktadır.",
                    CreateDate = DateTime.Now.Date,
                    Author = new Author()
                    {
                        AuthorId = 2,
                        IsActive = true,
                        CreateDate = DateTime.Now,
                        Name = "Mustafa",
                        Surname = "Erdoğan"
                    },
                    Category = new Category() { CategoryId = 2, Name = "Hukuk", IsActive = true, CreateDate = DateTime.Now.Date }

                });

            }
            return Articles;
        }

        public List<Article> GetArticlesByCatId(int catId)
        {
            throw new NotImplementedException();
        }

        public List<Article> GetArticlesWithCategoryAndAuthor()
        {
            if (Articles.Count <= 0)
            {
                Articles.Add(new Article()
                {
                    ArticleId = 1,
                    IsActive = true,
                    Title = "Ölçek Geliştirme Ve Uyarlama Çalışmalarının İncelenmesi",
                    Content = "Bu çalışmanın amacı Türkiye’de 2005-2014 yılları arasında hakemli dergilerde matematik eğitimi alanında yayınlanan ölçek geliştirme ve uyarlama çalışmalarının sahip olduğu örneklem büyüklüğü, madde sayısı, Cronbach Alpha katsayısı değerlerinin ve ölçek geliştirme ve uyarlama adımlarının karakteristik özelliklerinin incelenmesidir. Zengin veri elde etmeye odaklı nitel paradigmaya sahip olan bu araştırma yorumlayıcı yaklaşımı benimsemiştir. Özellikle matematik eğitiminde yapılan ölçek geliştirme ve uyarlama çalışmalarına odaklanması araştırmanın özel durum çalışması deseni ile yürütülmesine sebep olmuştur. İncelenen 54 derginin 24 tanesinde 35 ölçek geliştirme ve 18 ölçek uyarlama çalışması, Pearson korelasyon katsayısı testi ve Ölçek Geliştirme Formu, Ölçek İnceleme Formu kullanılarak kestirimsel ve betimsel istatistikler yardımıyla sunulmuştur. Araştırma sonucunda çalışmaların neredeyse tamamında Cronbach Alpha katsayılarının 0,80 değerinden fazla çıktığı, örneklem büyüklüğü ile madde sayısı arasında anlamlı bir ilişki bulunmadığı ve bazı araştırmalarda madde sayısına düşen uygulayıcı sayısının beşten az olduğu sonucuna ulaşılmıştır. Ayrıca ölçek geliştirme çalışmalarının %65,51’inde ölçek geliştirme adımlarının gerçekleştirildiği, ölçek uyarlama çalışmalarının %52,96’sında ölçek uyarlama adımlarının gerçekleştirildiği görülmüştür. Bununla birlikte ölçek geliştirme ve uyarlama çalışmalarında,  deneme uygulamanın ve geçerlik çalışmalarının yapılması gibi adımların gerçekleştirildiğine ilişkin bilgilerin raporlanmadığı ve dolayısıyla ve araştırmacıların ölçek geliştirme ve uyarlama çalışmalarına yeteri kadar ilgi göstermediği sonucuna ulaşılmıştır.",
                    CreateDate = DateTime.Now.Date,
                    Author = new Author() { AuthorId = 1, IsActive = true, CreateDate = DateTime.Now, Name = "Ali", Surname = "Delice" },
                    Category = new Category() { CategoryId = 1, Name = "Matematik Öğretimi", IsActive = true, CreateDate = DateTime.Now.Date }
                });

                Articles.Add(new Article()
                {
                    ArticleId = 2,
                    IsActive = true,
                    Title = "Ölçek Geliştirme Ve Uyarlama Çalışmalarının İncelenmesi",
                    Content = "Gerek demokrasi gerekse anayasal yönetim çağımızın önde gelen siyasî idealleri arasında yer almaktadır. “Anayasal demokrasi” terimi bu iki ideali birleştiren bir rejim modelini ifade etmektedir. Ancak bu terim aynı zamanda kendi içinde bir gerilimi de barındırmaktadır. Anayasacılıkla demokrasi arasındaki gerilimin en belirgin şekilde kendini gösterdiği alan “anayasa yargısı”dır. Anayasa yargısının demokratik meşruluğu sorunu son yıllarda Türkiye’de de tartışılmaktadır. Bu makalede Türkiye Anayasa Mahkemesi’yle ilgili asıl sorunun onun demokratik çoğunlukları anayasal yönetimin gerekleriyle sınırlamasından ziyade, ideolojik devlet iktidarı adına sınırlaması olduğu savunulmaktadır.",
                    CreateDate = DateTime.Now.Date,
                    Author = new Author()
                    {
                        AuthorId = 2,
                        IsActive = true,
                        CreateDate = DateTime.Now,
                        Name = "Mustafa",
                        Surname = "Erdoğan"
                    },
                    Category = new Category() { CategoryId = 2, Name = "Hukuk", IsActive = true, CreateDate = DateTime.Now.Date }

                });

            }
            return Articles;
        }

        public Article GetById(int id)
        {
            GetAllByFilterOrNotFiltered();
            if (Articles.Count>0)
            {
                return Articles.Where(x => x.ArticleId == id).FirstOrDefault();
            }
            return null;
        }

        public int Update(Article entity)
        {
            throw new NotImplementedException();
        }
    }
}
