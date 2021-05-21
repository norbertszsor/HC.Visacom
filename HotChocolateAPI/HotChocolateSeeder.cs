using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolateAPI.Entities;
using Microsoft.AspNetCore.Identity;

namespace HotChocolateAPI
{
    public class HotChocolateSeeder
    {
        private readonly HotChocolateDbContext _dbContext;
        public IPasswordHasher<User> _passwordHasher { get; }
        public HotChocolateSeeder(HotChocolateDbContext dbContext, IPasswordHasher<User> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }
        public void Seed()
        {

            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    foreach (var item in roles)
                    {
                        _dbContext.Add(item);
                        _dbContext.SaveChanges();
                    }
                }

                var user = _dbContext.Users.FirstOrDefault(x => x.Email == "admin@gmail.com");
                if (user == null)
                {
                    var newAdmin = CreateAdmin();
                    _dbContext.Users.Add(newAdmin);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Products.Any())
                {
                    var products = GetProducts();
                    _dbContext.AddRange(products);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.OrderStatuses.Any())
                {
                    var statuses = GetStatuses();
                    foreach (var item in statuses)
                    {
                        _dbContext.Add(item);
                        _dbContext.SaveChanges();
                    }
                }

                if (!_dbContext.Opinions.Any())
                {
                    _dbContext.Opinions.AddRange(GetOpinions());
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Orders.Any())
                {
                    _dbContext.Orders.AddRange(GetOrders());
                    _dbContext.SaveChanges();
                }
            }
        }
        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name="Customer"
                },
                new Role()
                {
                    Name="Warehouseman"
                },
                new Role()
                {
                    Name="Admin"
                },
                new Role()
                {
                    Name="Blogger"
                }
            };
            return roles;
        }
        private User CreateAdmin()// created admin account just for testing (hard coded).
        {
            var newAdmin = new User()
            {
                Email = "admin@gmail.com",
                FirstName = "Visa",
                LastName = "Com",
                IsActivated = true,
                RoleId = 3,
                PhoneNumber = "123456789",
                Address = new List<Address>()
                
            };
            newAdmin.Address.Add(new Address()
            {
                Town = "Olsztyn",
                Street = "Słoneczna",
                HouseNumber = "54",
                PostalCode = "10-710"
            });

            var password = "admin1";

            var hashedPassword = _passwordHasher.HashPassword(newAdmin, password);

            newAdmin.PasswordHash = hashedPassword;

            return newAdmin;
        }
        private List<Product> GetProducts()
        {
            var products = new List<Product>()
            {
                new Product()
                {
                    Name = "Codiaeum variegatum “Petra”",
                    Price = 30,
                    Description = "Codiaeum variegatum „Petra” to dość wymagająca roślina. Gubi liście gdy tylko poczuje się nieszczęśliwa. Warto poeksperymentować ze znalezieniem dla niej idealnego pod względem warunków świetlnych stanowiska w naszym domu, dzięki czemu jej liście rozwiną w pełni swój potencjał intensywności wybarwienia.",
                    Amount = 100
                },
                new Product()
                {
                    Name = "Aglaonema Red Master",
                    Price = 70,
                    Description = "Niezwykle dekoracyjna odmiana aglaonemy, która zachwyca intensywnym różowym wybarwieniem, pokrywającym znaczną część liści i delikatnie zaznaczonymi na zielono brzegami. Roślina idealnie sprawdzi się dla początkujących, którzy jednocześnie poszukują bardziej oryginalnych okazów.",
                    Amount = 100
                },
                new Product()
                {
                    Name = "Alocasia Black Velvet",
                    Price = 65,
                    Description = "Rzadko dostępna odmiana alokazji o aksamitnych, fałdowanych liściach o ciemnym wybarwieniu. Głęboki kolor liści tej rośliny nadaje jej mrocznego wdzięku i elegancji. Niemal czarna blaszka liściowa tworzy zachwycający efekt, kontrastując z jasnym unerwieniem.",
                    Amount = 100
                },
                new Product()
                {
                    Name = "Anthurium crystallinum",
                    Price = 500,
                    Description = "Połyskujące zielone i sercowate liście na długich ogonkach sięgają od 20 do 40 cm. Roślina ta wyróżnia się brunatno-zielonymi, dużymi, owalnymi liśćmi z wyraźnym białym rysunkiem.",
                    Amount = 100
                },
                new Product()
                {
                    Name = "Palma Areca",
                    Price = 200,
                    Description = "Bardzo atrakcyjna palma, z potencjałem na imponujący wzrost. W domowej uprawie może osiągnąć nawet 2 metry. Przywołuje klimat kolonialnego salonu jak i wakacji w ciepłych krajach. W dodatku jest bezpieczna dla zwierząt i prosta w pielęgnacji.",
                    Amount = 100
                },
                new Product()
                {
                    Name = "Asparagus Plumosus",
                    Price = 70,
                    Description = "Nie przycinany rośnie bardzo szybko i zaanektuje każdą przestrzeń. Ta nieco zapomniana roślina ucieszy miłośników miejskiej dżungli, bo wystarczy pozwolić jej spontanicznie rosnąć by w krótkim czasie cieszyć się jej bujnym wyglądem.",
                    Amount = 100
                },
                new Product()
                {
                    Name = "Asplenium antiquum",
                    Price = 40,
                    Description = "Bardzo dekoracyjna paproć o rozetowym pokroju i żywej zieleni liści. Jest bezpieczna dla zwierząt i zalicza się do roślin oczyszczających powietrze z zanieczyszczeń. By pięknie rosła warto wspomóc ją nawozem dla paproci i nie dopuszczać do nadmiernego przesuszenia gleby.",
                    Amount = 100
                },
                new Product()
                {
                    Name = "Begonia Amphioxus",
                    Price = 70,
                    Description = "Tropikalna piękność o podłużnych liściach, ozdobionych bordowymi plamkami. Wymaga specjalnego traktowania i wysokiej wilgotności. Nada się do domowej szklarni lub terrarium. Polecana raczej doświadczonym kolekcjonerom.",
                    Amount = 100
                },
                new Product()
                {
                    Name = "Blechnum gibbum Silver Lady",
                    Price = 30,
                    Description = "Wyjątkowo wdzięczna odmiana paproci, pożyteczna jako „zielony filtr”, przyjazna zwierzętom i idealna do sypialni lub salonu. Nie powinna ona znajdować się w pobliżu grzejników czy drzwi lub okien, a także być zacieniona. Paproć nie znosi opalania. Jej wrażliwe liście łatwo ulegają poparzeniom.",
                    Amount = 100
                },
                new Product()
                {
                    Name = "Calathea Freddie",
                    Price = 30,
                    Description = "Calathee to zachwycające mieszkanki tropikalnych dżungli. Są piękne, przyjazne dla zwierząt i choć uchodzą za trudne w obsłudze. Kluczowe jest zapewnienie im wysokiej wilgotności powietrza i podlewanie przegotowaną, najlepiej przefiltrowaną wodą. Są bardzo wrażliwe na „zimny prysznic” z kranówki. Na wieczór ich liście wznoszą się ku górze jak w modlitwie, stąd nazywane są roślinami modlitewnymi.",
                    Amount = 100
                },
                new Product()
                {
                    Name = "Clusia rosea Princess",
                    Price = 50,
                    Description = "Clusia rosea Princess, inaczej kluzja różowa, to roślina o bujnym, drzewkowatym pokroju, przypominająca fikusa. W domowych warunkach osiąga około 60cm wysokości, dlatego z pewnością nie opanuje nam całego mieszkania i zostawi też trochę miejsca dla innych roślin.",
                    Amount = 100
                },
                new Product()
                {
                    Name = "Chlorophytum comosum Ocean",
                    Price = 30,
                    Description = "Urokliwa i pożyteczna. Znajduje się na liście 18 roślin, które NASA wytypowało jako szczególnie efektywne w oczyszczaniu powietrza w naszych domach. Przy tym jest prosta w pielęgnacji, mało wymagająca, wytrzymała i przyjazna dla zwierząt.",
                    Amount = 100
                },
                new Product()
                {
                    Name = "Colocasia esculenta Cintho Aloha",
                    Price = 130,
                    Description = "Rzadko spotykana Colocasia o wspaniałym wybarwieniu. Ciemne, zamaszyste liście rozjaśnia neonowo zielony nerw główny. Podobnie jak wszystkie Colocasie „Esculenta”, jej bulwa jest cenionym składnikiem kuchni wysp Pacyfiku. Aby Colocasia Cintho Aloha cieszyła nas przez lata musimy zapewnić jej podwyższoną wilgotność powietrza. Ta tropikalna roślina jest wrażliwa na przesuszenie. Należy ją nawozić i dopieszczać, a po zakupie przesadzić w przepuszczalne, lekkie podłoże.",
                    Amount = 100
                },
                new Product()
                {
                    Name = "Dieffenbachia Reflector",
                    Price = 100,
                    Description = "Wyjątkowo dekoracyjna odmiana dieffenbachii, której liście zachwycają ciemnozielonym odcieniem i kontrastującymi, neonowymi plamkami. Na uwagę zasługuje również wyrazisty, jasny nerw przechodzący wzdłuż blaszki.",
                    Amount = 100
                },
                new Product()
                {
                    Name = "Dracaena marginata bicolor",
                    Price = 300,
                    Description = "Gęsty pióropusz ostro zakończonych liści to jej znak rozpoznawczy. Draceny od lat wnoszą do naszych przestrzeni nutę tropikalnej egzotyki. Dodatkowo jest jedną z 18 roślin, które NASA umieściła na liście szczególnie efektywnych w filtrowaniu powietrza ze szkodliwych substancji.",
                    Amount = 100
                },
                new Product()
                {
                    Name = "Ficus americana tresor",
                    Price = 150,
                    Description = "Dostojny fikus o nieco bardziej wydłużonych liściach. Naturalny oczyszczacz powietrza. Łatwy w pielęgnacji, więc będzie idealny również dla tych początkujących roślinomaniaków.",
                    Amount = 100
                },
                new Product()
                {
                    Name = "Fittonia Pink Special",
                    Price = 20,
                    Description = "Fittonie to wdzięczne w hodowli, śliczne rośliny o szałowym, zwykle kontrastowym wybarwieniu. Są przyjazne dla zwierząt, więc bez obaw możemy je eksponować w każdym miejscu domu. Fittonie lubią wilgoć i ciepło i doskonale odnajdą się też jako mieszkanki lasów w słoiku czy terrariów.",
                    Amount = 100
                },
                new Product()
                {
                    Name = "Monstera Deliciosa",
                    Price = 130,
                    Description = "Tropikalny klasyk o fantazyjnych, sercowatych liściach. Jeżeli marzy Ci się domowa dżungla, monstera deliciosa to absolutny must-have. Rozrasta się jak szalona i świetnie prezentuje w salonie, na patio czy w subtelnie zacienionej sypialni. Ma umiarkowane wymagania; lubi ciepło, wilgotne powietrze i ostrożne podlewanie. Stosowanie kilku prostych zasad pielęgnacyjnych sprawi, że odwdzięczy się imponującym, monstrualnym wzrostem.",
                    Amount = 100
                },
                new Product()
                {
                    Name = "Pachira Aquatica (Pachira Wodna)",
                    Price = 200,
                    Description = "W naturze rośnie do 20 metrów i rodzi jadalne owoce. W uprawie domowej daleko jej do tych rozmiarów, ale wciąż pozostaje piękną rośliną doniczkową o dłoniastych liściach i ciekawym, egzotycznym wyglądzie. Nazywana jest też drzewkiem szczęścia i pieniędzy. W swojej ojczyźnie – Ameryce Południowej, rośnie na terenach zalewowych, stąd jej nazwa.",
                    Amount = 100
                },
                new Product()
                {
                    Name = "Peperomia verticillata Red Log",
                    Price = 35,
                    Description = "Czarująca odmiana peperomii, w której zachwyca kontrast między ciemną zielenią blaszki liściowej, a bordowym spodem. Młode sadzonki mają zwarty, krępy pokrój, ale wraz ze wzrostem ich pędy wydłużają się tworząc bardzo efektowną, ażurową formę.",
                    Amount = 100
                },
                new Product()
                {
                    Name = "Philodendron Birkin",
                    Price = 200,
                    Description = "Przykuwająca wzrok odmiana filodendrona o zwartym pokroju i przepięknie wybarwionych liściach. Dolne liście maja głęboki ciemnozielony kolor, a górne biało-zielony z wyraźnie zarysowanym paskowaniem.",
                    Amount = 100
                },
                new Product()
                {
                    Name = "Philodendron Pink Princess",
                    Price = 900,
                    Description = "Różówa księżniczka, kolekcjonerski kultywar filodendrona o bajecznych, różówych wariegecjach. Niekłopotliwa w uprawie.",
                    Amount = 100
                },
                new Product()
                {
                    Name = "Philodendron Scandens Lemon Lime",
                    Price = 60,
                    Description = "Rzadki philodendron o neonowo żółtych liściach w sercowatym, opływowym kształcie. Prosty w obsłudze i szybko rosnący, a jego wibrująca, żywa kolorystyka wniesie energię w każdą przestrzeń.",
                    Amount = 100
                },
                new Product()
                {
                    Name = "Pilea crassifolia",
                    Price = 20,
                    Description = "Mieszkanka podszycia tropikalnych lasów Ameryki Południowej. Wzrok przyciągają jej niesamowite, pomarszczone liście o wyżłobionej fakturze i ząbkowanych marginesach. Dekoracyjny efekt podkręcają jeszcze trzy wyraziste nerwy biegnące wzdłuż wyżłobień na blaszce liścia.",
                    Amount = 100
                },
                new Product()
                {
                    Name = "Rhipsalis Burchellii",
                    Price = 25,
                    Description = "Wczesną wiosną zakwita żółtawymi, drobnymi kwiatkami, a gdy te obumierają, w ich miejsce pojawiają się białe “owoce” przypominające kulki jemioły. Gdy pędy się znacznie wydłużą, jego bujną czuprynę można przycinać dla uzyskania zwartego pokroju.",
                    Amount = 100
                },
                new Product()
                {
                    Name = "Sansevieria Fernwood Mikado",
                    Price = 70,
                    Description = "Zawsze modna i ponadczasowa. Sansevieria wpisuje się w klimat wielu styli i wnętrz. Nie sprawia żadnych trudności pielęgnacyjnych i radzi sobie świetnie w każdych warunkach świetlnych..",
                    Amount = 100
                },
                new Product()
                {
                    Name = "Sansevieria Zeylanica Fan",
                    Price = 40,
                    Description = "Przydomek „Fan” zawdzięcza swej wachlarzowatej (ang. fan) formie. Płaskie, mięsiste liście o intensywnym, dekoracyjnym wybarwieniu układają się w fantazyjny kształt zielonego wachlarza.",
                    Amount = 100
                },
                new Product()
                {
                    Name = "Schefflera arboricola Gold Capella",
                    Price = 100,
                    Description = "Nakrapiana piękność rodem z Dalekiego Wschodu, która wymaga naprawdę niewiele. Splątane łodygi i kępiasty pokrój nadaje jej uroczy drzewiasty wygląd. Nie przepada za skupionym światłem – pod jego wpływem jej liście mogą żółknąć.",
                    Amount = 100
                },
                new Product()
                {
                    Name = "Strelitzia nicolai variegata",
                    Price = 900,
                    Description = "Majestatyczna strelicja w wersji z wariegacjami. Gratka dla miłośników gatunku, jak i rzadkich, kolekcjonerskich perełek. Nerwy rośliny są w kolorze mlecznobiałym, zamiast w tradycyjnie zielonym.",
                    Amount = 100
                },
                new Product()
                {
                    Name = "Zamioculcas Zenzi",
                    Price = 65,
                    Description = "Odmiana Zamioculcasa o bardzo grubych pędach i wzniesionych, drobnych, osadzonych blisko siebie listkach. Pędy u nasady mają beczułkowaty kształt, co będzie widoczne przy dalszym wzroście rośliny.",
                    Amount = 100
                }
            };
            return products;
        }
        private List<OrderStatus> GetStatuses()
        {
            var roles = new List<OrderStatus>()
            {
                new OrderStatus()
                {
                    Name="W trakcie realizacji"
                },
                new OrderStatus()
                {
                    Name="Czeka na nadanie"
                },
                new OrderStatus()
                {
                    Name="Przekazane kurierowi"
                },
                new OrderStatus()
                {
                    Name="Zakończone"
                },
                new OrderStatus()
                {
                    Name="Anulowano"
                }
            };
            return roles;
        }
        private List<Opinion> GetOpinions()
        {
            Random rnd = new Random();
            var len = _dbContext.Products.ToList().Count + 1;
            var list = new List<Opinion>()
            {
                new Opinion()
                {
                    ProductId = rnd.Next(len),
                    Stars = rnd.Next(4) + 1,
                    DescriptionOfOpinion = "Kozak",
                    Date = DateTime.Now.ToShortDateString(),
                    UserId = 1
                },
                new Opinion()
                {
                    ProductId = rnd.Next(len),
                    Stars = rnd.Next(4) + 1,
                    DescriptionOfOpinion = "Moja babcia ma i poleca",
                    Date = DateTime.Now.ToShortDateString(),
                    UserId = 1
                },
                new Opinion()
                {
                    ProductId = rnd.Next(len),
                    Stars = rnd.Next(4) + 1,
                    DescriptionOfOpinion = "Szybko usycha",
                    Date = DateTime.Now.ToShortDateString(),
                    UserId = 1
                },
                new Opinion()
                {
                    ProductId = rnd.Next(len),
                    Stars = rnd.Next(4) + 1,
                    DescriptionOfOpinion = "Bez szału",
                    Date = DateTime.Now.ToShortDateString(),
                    UserId = 1
                },
                new Opinion()
                {
                    ProductId = rnd.Next(len),
                    Stars = rnd.Next(4) + 1,
                    DescriptionOfOpinion = "Polecam",
                    Date = DateTime.Now.ToShortDateString(),
                    UserId = 1
                },
                new Opinion()
                {
                    ProductId = rnd.Next(len),
                    Stars = rnd.Next(4) + 1,
                    DescriptionOfOpinion = "Trzeba często podlewać",
                    Date = DateTime.Now.ToShortDateString(),
                    UserId = 1
                },
                new Opinion()
                {
                    ProductId = rnd.Next(len),
                    Stars = rnd.Next(4) + 1,
                    DescriptionOfOpinion = "Słabe",
                    Date = DateTime.Now.ToShortDateString(),
                    UserId = 1
                },
                new Opinion()
                {
                    ProductId = rnd.Next(len),
                    Stars = rnd.Next(4) + 1,
                    DescriptionOfOpinion = "Bez nawozu nie urośnie",
                    Date = DateTime.Now.ToShortDateString(),
                    UserId = 1
                }
            };
            return list;
        }
        private List<Order> GetOrders()
        {

            Random rnd = new Random();
            var list = new List<Order>();
            for (int i = 0; i < 10; i++)
            {
                List<int> idProducts = new List<int>();
                idProducts.Add(rnd.Next(30));
                idProducts.Add(rnd.Next(30));
                var products = _dbContext.Products.Where(x => idProducts.Contains(x.Id));
                var order = new Order()
                {
                    UserId = 1,
                    AddressId = 1,
                    Date = DateTime.Now.ToShortDateString(),
                    OrderStatusId = rnd.Next(5) + 1,
                    Products = products.ToList(),
                    OrderAmountProducts = new List<OrderAmountProducts>()
                };
                decimal suma = 0;
                foreach (var item in products)
                {
                    var ilosc = rnd.Next(5);
                    item.Amount -= ilosc;
                    suma += ilosc * item.Price;
                    order.OrderAmountProducts.Add(new OrderAmountProducts
                    {
                        ProductId = item.Id,
                        Amount = ilosc
                    });
                }
                order.TotalCost = suma;
                list.Add(order);
            }
            return list;
        }
    }
}