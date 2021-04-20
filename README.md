# ArticleAPIApplication


Sorular: 
- Projede kullanıdığınız tasarım desenleri hangileridir? Bu desenleri neden kullandınız? 
Temelde tasarım deseni olarak Generic Repository tasarım desenini kullandım. Repository Design Pattern,
veritabanı sorumluluğunu üstlenen sınıfı tasarlarken bir standart üzerine oturtmayı sağlayan bir tasarım desenidir.
Ve bu projede temelde tüm entityler için CRUD işlemlerini herbir entity için DAL katmanında ayrı ayrı doldurmak yerine Generic bir class’ı
temel olarak ayarlayıp daha sonrasında bu class’ı inherit eden entity DAL classlarını oluşturarak kalıtım yoluyla veri tabanı işlemlerimizi
tek bir noktadan yönetmiş oluruz. Ayrıca burada bileşenler arası bağımlılığı en aza indirerek (SOLID prensiplerinden Dependency Inversion
prensibine uygun olarak) bir tasarım kullanmaya çalıştım ve bunun sonucunda da bileşenler arası nesne üretim işlemini .Net Core’un
beraberinde gelen (IoC kullanmaya ihtiyaç kalmıyor (Ninject vs)) özellik ile Dependency Injection tasarım desenini de uygulayabilmiş oldum.
Dependency Injection tasarım desenini kullanma sebebini de açıkayacak olursam, örneğin ben bu projem de ORM lerden EfCore kullandım ve 
EfCoreGenericRepository classım Irepository interface’ini implemente ediyor. Ve bunun yanında da EfCoreArticleDAL (Article entity’isinin veritabanı
işlemlerinin yürütüleceği en alt sınıf diyebiliriz) IArticleDAL interface’ini implemente ediyor ve EfCoreGenericRepository classını inherit ediyor.
Ve son olarak ta startup.cs de projeye IArticleDAL’ı gördüğünde EfCoreArticleDAL nesnesinden instance al diyoruz temelde. Ama ben ihtiyaçlarım doğrultusunda 
farklı Micro orm olarak Dapper kullanmaya karar verdim. Burada teknoloji değişikliğine gitmek istedğimizde bir tight coupling yapmadığımız için kolay bir şekilde değişikliğe
gidebilirim örneği article entity’si üzerinden devam ettirecek olursam yeni bir dapper için genericrepository class’ı oluşturulup Irepository interface’i implemente edilir 
ve EfCoreArticleDAL classı yerine bir class daha oluşturup DapperArticleDAL oluştururuz ve bu class’ada IArticleDAL interface’ini implemente ederiz. Ve son olarakta 
startup.cs den IArticleDAL’ı gördüğünde  DapperArticleDAL oluştur diyerek teknoloji değişikliğine gitmiş oluruz. Ayrıca burada tabiki tüm bunlar düşünüldüğinde Open-Closed
prensibine de uymuş oluruz. Hiçbir classda veya methodta değişiklik yapmadık ama tasarımımız geliştirilebilir olduğundan değşime kapalı gelişime açık tutmuş olduk projemizi.

- Kullandığınız teknoloji ve kütüphaneler hakkında daha önce tecrübeniz oldu mu? Tek tek yazabilir misiniz? 
-.Net Core ile öncesinde kendimi geliştirmek adına mini e-ticaret sitesi örneği yaptım.
-ORM olarak EF’yi daha önce projelerimde kullandım. Beraberinde Linq ‘ide tabiki. Proje oluşturulurken tabiki  Ef’nin Code-First yaklaşımı benimsenerek
yazdım daha öncesinde Db-First kullanmıştım.
-WebAPI yi işten sonraki zamanlarım boyunca kendimi geliştirmek adına öğrenmiştim. Ama iş hayatım da WCF kullanılan projeleri inceledim.
- Mssql kullandım daha önceki projelerimde ve iş hayatımda daki uygulamama da ADO.Net ile dataAccess sağlandığından Mssql ile daha detaylı çalışma fırsatım oldu ve sql dili ile.  . 

- Daha geniş vaktiniz olsaydı projeye neler eklemek isterdiniz? - Eklemek istediğiniz bir yorumunuz var mı?
Proje temelde mini bir anasayfa dan ve article ekleme,silme ve güncelleme den oluşuyor.
Ben test verisi sistem çalıştığında yazarları ve kategorileri veri tabanına eklesin diye bir static class üzeinden veri tabanına eklenecek şekilde koydum. 
Tabi bunlar proje içerisinde eklenebilir olmalı. Sayfalama işlemleri ve üyelik sistemi mevcut değil projede bunlar eklenebilir. 

Eklemek istediğim yorum :
APIden beslenen MVC.UI katmanıda ekledim projeye case'de bu şekilde yapılması gerek diye anladım.
