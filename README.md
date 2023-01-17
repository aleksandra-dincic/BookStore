# BookStore

Aplikacija simulira rad biblioteke.

Aplikacija se sastoji od sledecih stranica: 
 
**Home page** - Sastoji se od statickih delova koju cine testimonials, blog deo, contact us i *featured books*
gde se prikazuju knjige direktno iz baze ciji property je  IsFeatured=true u bazi. 
Ispod postoji dugme More Books koje nas vodi do sledece stranice. 

**Books page** - Stranica gde se prikazuju sve knjige iz baze podataka. Koja god knjiga se doda u bazu podataka, pokazace se ovde.  Kada se klikne na neku od knjiga, vodi nas do sledece stranice. 

**Book/One Page** – Prikaz jedne knjige. Ova stranica prikazuje detalje jedne knjige. Vadi iz baze sve podatke za tu knjigu na osnovu ID-a koji se prosledi. Moze se videte naslov knjige, slika, cena, autori i opis knjige.  Takodje postoji polje Quantity I dugme Add To Cart. 
Ovde korisnik moze da “kupi” knjigu tako sto upise broj primeraka I klikne na Add To Cart.  
Kada se knjiga doda u korpu korisnik je prebacen na stranicu Korpe. 
 
**Checkout** - Stranica prikazuje korpu, tj. Sve dodate knjige koje hoce korisnik da kupi. Klikom na Finish, knjige se kupuju I korpa se prazni.  

**Admin strana** - Ovo je strana koju korisnik ne vidi. Sluzi za simulaciju admin panela. Ovde je moguce dodavari nove, brisati ili editovati postojece knjige. Url je: /Admin/Index. 

Soultion se sastoji iz 4 projekta:
  1. Domain: nalaze se entiteti nad kojima se povezujemo sa bazom podataka
  2. Persistence: repozitorijumi koji koriste konekciju sa bazom i manipulaziju nad podacima
  3. Application: biznis logika
  4. WebPlatform: MVC projekat
 
Instalirati: 
 1. Visual Studio (2019 ili 2022), mozda moze i 2017 
 2. .NET 6.0 (https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-6.0.403-windows-x64-installer) 
 3. MongoDB (https://www.mongodb.com/try/download/community) 
