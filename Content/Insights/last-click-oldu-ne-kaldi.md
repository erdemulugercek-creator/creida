---
slug: last-click-oldu-ne-kaldi
title: Last-click öldü. Şimdi ne ölçüyoruz?
subtitle: Cookie sonrası attribution kâhinleri sussun; ölçüm üç katmana indi.
author: Giray Can Muştu
date: 2026-01-30
readingTime: 7 dk okuma
category: Veri
---

Son üç yılda attribution dünyası altüst oldu: Apple ATT, Chrome'un cookie planı (defalarca ertelendi ama sürmüyor), Avrupa'da GDPR'ın daha sıkı yorumları. Sonuç: **last-click attribution güvenilir bir ölçüm sistemi olmaktan çıktı.**

Bu yeni bir haber değil. Yeni haber şu: ajansların çoğu hâlâ rapor formatını değiştirmedi. ROAS hâlâ "x'ten geldi", CAC hâlâ "bu kanaldan" diye raporlanıyor. Veri eksildikçe rakam abartıldı; çünkü eksik veri tahmine zorlar.

Bizim için ölçüm 2026'da üç katmana indi. Hangisini hangisi için kullandığımızı net biliyoruz; aralarındaki farkı müşteriye açıklıyoruz.

## Katman 1: Platform içi (Meta, Google, TikTok, vs.)

Bu katman, **platformun ne dediğine bakmak** için kullanılır. Meta'nın size raporladığı dönüşüm, gerçeğin %30-70'i kadarıdır (sektöre, fraud düzeyine ve attribution penceresine göre değişir). Ancak bu katman, **iki kreatif arasında karşılaştırma yapmak** için hâlâ değerlidir.

> Kullanım: A/B testleri, kreatif seçimi, bid kararları.
> Kullanılmamalı: Toplam kanal performansı, bütçe paylaşımı kararı.

## Katman 2: First-party (CRM, analytics, sunucu tarafı)

Sitenize gelen trafiğin nereden geldiğini server-side event'lerle takip etmek. UTM parametreleri, CRM'e ilk kaynak bilgisi, kohort analizi. Bu katman gerçek dünyayı daha iyi yansıtır ama her aksiyon için bir kaynak atayamaz.

> Kullanım: Kanal seviyesinde bütçe paylaşımı, kohort lifetime value, içerik etkisi.
> Kullanılmamalı: "Bu reklam bu satışı yaptı" iddiası.

## Katman 3: Marka soruları (post-purchase survey, brand lift study, incrementality test)

En değerli ama en az kullanılan katman. Sipariş onay sayfasında "Bizi nereden duydunuz?" sorusu, kampanya öncesi/sonrası brand awareness araştırması, kampanyayı kapalı tutarak yapılan **incrementality testi**.

Incrementality testi şudur: aynı kampanyayı bir coğrafyada bir hafta kapalı tutar, satışların ne kadar düştüğünü ölçersin. Eğer satışlar düşmediyse, o kampanya zaten olacak olanı tekrar reklamlıyordu; iptal et.

> Kullanım: Markanın gerçek değerini ölçmek, "tracker'ın bana yalan söyleyip söylemediğini" doğrulamak.
> Kullanılmamalı: Haftalık optimizasyon kararı için. Yavaş çalışır.

## Üç katman ne işe yarar?

Tek bir katmana bakan rapor yalan söyler. Üçü birden bakıldığında **kanaat oluşur** — yine kesinlik değil, kanaat. Ama bu kanaat, müşterinin bütçesini doğru paylaştırmak için yeterli.

Bizim haftalık raporumuz şu üç sayfa:

1. Platform içi kreatif performansı (hangi varyantı durdurduk, hangisini ölçeklendirdik)
2. First-party kanal payı (Google Analytics + CRM kohort)
3. Aylık brand lift / quarterly incrementality testi (her hafta değil, ama varsa rapora girer)

Last-click öldü; yerine matematik gelmedi. Yerine **karar disiplini** geldi. Ölçemediğin şeye bütçe ayırmaktan korkmuyoruz; ölçtüğümüz şeylere körlemesine güvenmiyoruz da.
