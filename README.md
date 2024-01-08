Genel Bilgiler

Bir e-ticaret sitesi tasarladım. Ürünler veri tabanında tutuluyor. Ana sayfa, arama sayfası, ürün detay sayfası ve sepet sayfası olmak üzere 4 sayfa bulunuyor. Veri tabanı olarak ise Firebase kullanılıyor. Her şeyi sıfırdan yapmak istedim ve hazır bir veri tabanı kullanmadım. Dolayısıyla sınırlı sayıda ürün (plak) bulunuyor. Sitenin her sayfasında bulunan “layout” 2 kısımdan oluşuyor. Başlık kısmında solda sitenin ana sayfasına yönlendirme yapan bir buton, ortada arama sayfasına yönlendiren bir arama çubuğu ve sağda sepet sayfasına yönlendirme yapan sepet ikonu bulunuyor.


Ana Sayfa

Ana sayfada en popüler 3 ürün görüntüleniyor. Bunun için arka yüzde veri tabanından tüm ürünler çekilip, “review” miktarlarına göre sıralandıktan sonra ilk 3 ürün seçiliyor ve ön yüze gönderiliyor. Ön yüzde ise her ürünün bilgileri ve fotoğrafları bulunuyor. Ayrıca ürüne tıklandığında, ürünün detay sayfasına yönlendirme yapılıyor.
 
 
Arama Sayfası

Arama sayfasında, aranan sözcüğü, albüm ya da sanatçı adında bulunduran ürünler listeleniyor. Bunun için arka yüzde veri tabanından tüm ürünler çekilip, “name” ve “artist” sütunlarında, aranan sözcük (query) olup olmadığı kontrol ediliyor. Şartları sağlayan kayıtlar ön yüze gönderiliyor. Ayrıca ürünlerin hemen üstünde, sıralama yapmak için 2 açılır liste girdisi ve bir buton bulunuyor. Bu girdilerin ilkinden sıralama yapılacak sütun, ikincisinden ise sıralama şekli (artan ya da azalan) seçiliyor. Seçimler yapılıp sırala (sort) butonuna basıldığında, arka yüzdeki SortProducts() metodu ile ürünler sıralanıyor. Bu metod, bir switch ve koşul yapısı ile istenilen sıralamayı yapıp yeni listeyi ön yüze gönderiyor. Aramalarda ve URL’lerde Türkçe karakterler sorun çıkarabileceği için, arka yüzde ReplaceTurkishCharacters() isimli bir metod ve ön yüzde aynı isimde bir fonksiyon bulunuyor. Bunlar, bir sözlük yapısı kullanarak Türkçe karakterleri İngilizce muadilleriyle değiştiriyorlar. Ana sayfada olduğu gibi, tüm ürünlerin çeşitli bilgileri görüntüleniyor ve ürüne tıklamak ürünün detay sayfasına yönlendirme yapıyor.
 
 
Ürün Detay Sayfası

Ürün detay sayfasında, seçilen ürünün bilgileri yer alıyor. Diğer sayfalardan farklı olarak, detay sayfasında ürünün fotoğrafı daha büyük ve fazladan detay bilgileri bulunuyor. Ürün bilgilerinin yanında ise ürünü sepete eklemek ya da sepetten kaldırmak için kullanılan bir buton bulunuyor. Detay sayfasının URL kısmında, sayfa adından sonra ürün ID değeri bulunuyor. ID değeri her ürün için eşsiz olduğundan, ürün bilgileri bu ID aracılığıyla getiriliyor. Arka yüzde, veri tabanından çekilen ürünler içinden, ID değeri eşleşen ürün bilgileri ön yüze gönderiliyor. Ayrıca ürün sepete eklenilip çıkartıldığında, sepet bilgisini güncelleyen bir metod (bu metod ön yüzdeki ajax-get işlemini karşılamakta) da yine arka yüzde bulunuyor. Ön yüzde ise ürün bilgilerinin yanı sıra, Javascript ile yazılmış 2 fonksiyon bulunuyor. checkCartStatus() fonksiyonu, ürünün sepette olup olmadığını kontrol edip buna uygun olarak sepete ekle butonunu kırmızı ya da yeşil yapıyor ve üzerindeki yazıyı güncelliyor. Bu fonksiyon aynı zamanda sayfa yüklendiğinde otomatik olarak çalışıyor. addToCart() fonksiyonu, sepete ekle butonuna basıldığında çalışıyor. Ön yüzden alınan sepet bilgisi ve ürün ID değeri karşılaştırılıyor. Eğer ürün sepette ise, sepet bilgisinden, ürün ID değeri çıkarılıyor ve sepet bilgisi ajax aracılığıyla arka yüze gönderiliyor. Arka yüzde kullanıcının sepeti güncelleniyor. Eğer ürün sepette değil ise ürün ID değeri sepet bilgisine ekleniyor ve aynı işlemler yapılıyor.
 
 
Sepet Sayfası

Sepet sayfasında kullanıcının sepetine eklediği ürünler görüntüleniyor. Ürün detay sayfasında sepete eklenen ürünlerin ID değerleri HttpContext.Session içerisinde tutuluyor. Arka yüzde, tüm ürünler içinden, ID değeri HttpContext.Session içerisinde bulunan ürünler filtreleniyor ve ön yüze gönderiliyor. Ön yüzde ise yine diğer sayfalarda olduğu gibi ürünlerin bilgileri ve fotoğrafları bulunuyor, tıklandığında ürün detay sayfasına yönlendirme yapılıyor. Ayrıca sepetteki ürünlerin altında, ürünlerin toplam fiyatı yazıyor.
