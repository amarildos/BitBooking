namespace BitBooking.DAL.Migrations
{
    using BitBooking.DAL.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BitBooking.DAL.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BitBooking.DAL.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            ////////////// OUR SEED - BOOKING SEED

            context.AccomodationTypes.AddOrUpdate(
               x => x.AccomodationTypeId,
               new AccomodationType { AccomodationTypeId = 1, AccomodationTypeName = "Hotel" },
                //new AccomodationType { AccomodationTypeId = 2, AccomodationTypeName = "Motel" },
               new AccomodationType { AccomodationTypeId = 3, AccomodationTypeName = "Hostel" }
                //new AccomodationType { AccomodationTypeId = 4, AccomodationTypeName = "Apartment" }
               );

            context.AccomodationInfoes.AddOrUpdate(
                x => x.AccomodationInfoId,
                new AccomodationInfo { AccomodationInfoId = 1, AccomodationId = 1, Address = "Skenderija 1", City = "Sarajevo", Country = "Bosnia and Herzegovina", Email = "info@courtyard.ba", Phone = "+387 33 954-500", PostalCode = "71000" },
                new AccomodationInfo { AccomodationInfoId = 2, AccomodationId = 2, Address = "1 Kensington Ct, London W8 5DL", City = "London", Country = "United Kingdom", Email = "bookms@rchmail.com", Phone = "+44 20 7917 1000", PostalCode = "W8 5DL" },
                new AccomodationInfo { AccomodationInfoId = 3, AccomodationId = 3, Address = "Honegg, 6373 Ennetbürgen", City = "Ennetbürgen", Country = "Switzerland", Email = "nfo@villa-honegg.ch", Phone = "+ 41 41 618 32 00", PostalCode = "6373" },
                new AccomodationInfo { AccomodationInfoId = 4, AccomodationId = 4, Address = " Ferhadija 21", City = "Sarajevo", Country = "Bosnia and Herzegovina", Email = "hostel@vagabond.ba", Phone = "+387 33 238-811", PostalCode = "71000" },
                new AccomodationInfo { AccomodationInfoId = 5, AccomodationId = 5, Address = " 222 West 23rd Street", City = "New York", Country = "USA", Email = "hotel@chelsea.ba", Phone = "+1 616-918-8770", PostalCode = "NY 10011" },
                new AccomodationInfo { AccomodationInfoId = 6, AccomodationId = 6, Address = " Ul. Frana Supila 12", City = "Dubrovnik", Country = "Croatia", Email = "hostel@excelsior.ba", Phone = "+385 20 300 300", PostalCode = "20000" }
                );

            context.AccomodationServiceTypes.AddOrUpdate(
                x => x.AccomodationServiceTypeId,
                new AccomodationServiceType { AccomodationServiceTypeId = 1, Name = "General" },
                new AccomodationServiceType { AccomodationServiceTypeId = 2, Name = "Outdoor" },
                new AccomodationServiceType { AccomodationServiceTypeId = 3, Name = "Languages Spoken" },
                new AccomodationServiceType { AccomodationServiceTypeId = 4, Name = "Activities" }
                );

            context.Accomodations.AddOrUpdate(
                x => x.AccomodationId,
                new Accomodation { AccomodationId = 1, AccomodationName = "Hotel Courtyard", StarRating = 5, NumberOfRooms = 70, AccomodationTypeId = 1, AccomodationInfoId = 1, Description = "Whether traveling for business or pleasure, Courtyard by Marriott Sarajevo makes all of your travels successful by providing exactly what you need: spacious and thoughtful guest rooms, in the heart of the city." },
                new Accomodation { AccomodationId = 2, AccomodationName = "Milestone Hotel London", StarRating = 3, NumberOfRooms = 47, AccomodationTypeId = 1, AccomodationInfoId = 2, Description = "The exquisite five-star hotel is located within the esteemed Royal Borough of Kensington and Chelsea, overlooking Kensington Palace and Gardens, minutes from the Royal Albert Hall and with easy access to the West End." },
                new Accomodation { AccomodationId = 3, AccomodationName = "Hotel Villa Honegg", StarRating = 4, NumberOfRooms = 55, AccomodationTypeId = 1, AccomodationInfoId = 3, Description = "The Villa Honegg is a unique 5-star superior establishment in the heart of Switzerland on mount Bürgenstock. This long-established hotel was built in 1905 and reopened for business in May 2011 following a major refit. " },
                new Accomodation { AccomodationId = 4, AccomodationName = "Hostel Vagabond Sarajevo ", StarRating = 4, NumberOfRooms = 20, AccomodationTypeId = 3, AccomodationInfoId = 4, Description = "Vagabond is a new and unique Hostel at the best location in Sarajevo. Our completely renovated premises with spacious and airy rooms, new bathrooms, and fresh aromatic linens are complemented by exceptionally comfortable ambient and fully committed staff." },
                new Accomodation { AccomodationId = 5, AccomodationName = "Hotel Chelsea ", StarRating = 5, NumberOfRooms = 130, AccomodationTypeId = 1, AccomodationInfoId = 5, Description = "Built in 1883 as a private apartment building, the Chelsea has a long and infamous past—not surprising considering it’s been home to artists and writers of every stripe since it opened a year later." },
                new Accomodation { AccomodationId = 6, AccomodationName = "Hotel Excelsior Dubrovnik", StarRating = 5, NumberOfRooms = 200, AccomodationTypeId = 1, AccomodationInfoId = 6, Description = "Recognised as one of the finest hotels in the Mediterranean  it is only when you visit that you can fully appreciate the spectacular setting: the tranquil gardens, four gourmet restaurants,  panoramic views of the Adriatic Sea, a private beach, back-dropped by the UNESCO-protected Old Town. " }
                );


            context.AccomodationServices.AddOrUpdate(
                x => x.AccomodationServiceId,
                //First Hotel
                new AccomodationService { AccomodationServiceId = 1, AccomodationId = 1, AccomodationServiceTypeId = 1, Name = "Newspapers" },
                new AccomodationService { AccomodationServiceId = 2, AccomodationId = 1, AccomodationServiceTypeId = 1, Name = "Safe" },
                new AccomodationService { AccomodationServiceId = 3, AccomodationId = 1, AccomodationServiceTypeId = 1, Name = "Elevator" },
                new AccomodationService { AccomodationServiceId = 4, AccomodationId = 1, AccomodationServiceTypeId = 1, Name = "Non-smoking Rooms" },
                new AccomodationService { AccomodationServiceId = 5, AccomodationId = 1, AccomodationServiceTypeId = 1, Name = "Heating" },
                new AccomodationService { AccomodationServiceId = 6, AccomodationId = 1, AccomodationServiceTypeId = 1, Name = "Familly Rooms" },

                new AccomodationService { AccomodationServiceId = 7, AccomodationId = 1, AccomodationServiceTypeId = 2, Name = "Outdoor Pool" },
                new AccomodationService { AccomodationServiceId = 8, AccomodationId = 1, AccomodationServiceTypeId = 2, Name = "Grounds" },
                new AccomodationService { AccomodationServiceId = 9, AccomodationId = 1, AccomodationServiceTypeId = 2, Name = "Sun Deck" },

                new AccomodationService { AccomodationServiceId = 10, AccomodationId = 1, AccomodationServiceTypeId = 3, Name = "English" },
                new AccomodationService { AccomodationServiceId = 11, AccomodationId = 1, AccomodationServiceTypeId = 3, Name = "German" },
                new AccomodationService { AccomodationServiceId = 12, AccomodationId = 1, AccomodationServiceTypeId = 3, Name = "French" },


                new AccomodationService { AccomodationServiceId = 13, AccomodationId = 1, AccomodationServiceTypeId = 4, Name = "Tennis court" },
                new AccomodationService { AccomodationServiceId = 14, AccomodationId = 1, AccomodationServiceTypeId = 4, Name = "Hiking" },
                new AccomodationService { AccomodationServiceId = 15, AccomodationId = 1, AccomodationServiceTypeId = 4, Name = "Pool Table" },

                //Second Hotel
                new AccomodationService { AccomodationServiceId = 16, AccomodationId = 2, AccomodationServiceTypeId = 1, Name = "Newspapers" },
                new AccomodationService { AccomodationServiceId = 17, AccomodationId = 2, AccomodationServiceTypeId = 1, Name = "Safe" },
                new AccomodationService { AccomodationServiceId = 18, AccomodationId = 2, AccomodationServiceTypeId = 1, Name = "Elevator" },


                new AccomodationService { AccomodationServiceId = 19, AccomodationId = 2, AccomodationServiceTypeId = 2, Name = "Outdoor Pool" },
                new AccomodationService { AccomodationServiceId = 20, AccomodationId = 2, AccomodationServiceTypeId = 2, Name = "Grounds" },
                new AccomodationService { AccomodationServiceId = 21, AccomodationId = 2, AccomodationServiceTypeId = 2, Name = "Sun Deck" },

                new AccomodationService { AccomodationServiceId = 22, AccomodationId = 2, AccomodationServiceTypeId = 3, Name = "English" },

                ////Third Hotel

                new AccomodationService { AccomodationServiceId = 23, AccomodationId = 3, AccomodationServiceTypeId = 1, Name = "Non-smoking Rooms" },
                new AccomodationService { AccomodationServiceId = 24, AccomodationId = 3, AccomodationServiceTypeId = 1, Name = "Heating" },
                new AccomodationService { AccomodationServiceId = 25, AccomodationId = 3, AccomodationServiceTypeId = 1, Name = "Familly Rooms" },

                new AccomodationService { AccomodationServiceId = 26, AccomodationId = 3, AccomodationServiceTypeId = 2, Name = "Grounds" },
                new AccomodationService { AccomodationServiceId = 27, AccomodationId = 3, AccomodationServiceTypeId = 2, Name = "Sun Deck" },

                new AccomodationService { AccomodationServiceId = 28, AccomodationId = 3, AccomodationServiceTypeId = 3, Name = "English" },
                new AccomodationService { AccomodationServiceId = 29, AccomodationId = 3, AccomodationServiceTypeId = 3, Name = "French" },

                new AccomodationService { AccomodationServiceId = 30, AccomodationId = 3, AccomodationServiceTypeId = 4, Name = "Playground for kids" },


                ////Fourth Hotel

                new AccomodationService { AccomodationServiceId = 31, AccomodationId = 4, AccomodationServiceTypeId = 1, Name = "Non-smoking Rooms" },
                new AccomodationService { AccomodationServiceId = 32, AccomodationId = 4, AccomodationServiceTypeId = 1, Name = "Heating" },
                new AccomodationService { AccomodationServiceId = 33, AccomodationId = 4, AccomodationServiceTypeId = 1, Name = "Familly Rooms" },


                new AccomodationService { AccomodationServiceId = 34, AccomodationId = 4, AccomodationServiceTypeId = 2, Name = "Sun Deck" },

                new AccomodationService { AccomodationServiceId = 35, AccomodationId = 4, AccomodationServiceTypeId = 3, Name = "English" },


             //Fifth Hotel
                new AccomodationService { AccomodationServiceId = 36, AccomodationId = 5, AccomodationServiceTypeId = 1, Name = "Newspapers" },
                new AccomodationService { AccomodationServiceId = 37, AccomodationId = 5, AccomodationServiceTypeId = 1, Name = "Safe" },
                new AccomodationService { AccomodationServiceId = 38, AccomodationId = 5, AccomodationServiceTypeId = 1, Name = "Elevator" },
                new AccomodationService { AccomodationServiceId = 39, AccomodationId = 5, AccomodationServiceTypeId = 1, Name = "Non-smoking Rooms" },
                new AccomodationService { AccomodationServiceId = 40, AccomodationId = 5, AccomodationServiceTypeId = 1, Name = "Heating" },
                new AccomodationService { AccomodationServiceId = 41, AccomodationId = 5, AccomodationServiceTypeId = 1, Name = "Soundproof Rooms" },

                new AccomodationService { AccomodationServiceId = 42, AccomodationId = 5, AccomodationServiceTypeId = 2, Name = "Grounds" },

                new AccomodationService { AccomodationServiceId = 43, AccomodationId = 5, AccomodationServiceTypeId = 3, Name = "English" },
                new AccomodationService { AccomodationServiceId = 44, AccomodationId = 5, AccomodationServiceTypeId = 3, Name = "French" },
                new AccomodationService { AccomodationServiceId = 45, AccomodationId = 5, AccomodationServiceTypeId = 3, Name = "Italian" },
                new AccomodationService { AccomodationServiceId = 46, AccomodationId = 5, AccomodationServiceTypeId = 3, Name = "Chinese" },

                new AccomodationService { AccomodationServiceId = 47, AccomodationId = 5, AccomodationServiceTypeId = 4, Name = "Pool Table" },

                 //Sixth Hotel
                new AccomodationService { AccomodationServiceId = 48, AccomodationId = 6, AccomodationServiceTypeId = 1, Name = "Newspapers" },
                new AccomodationService { AccomodationServiceId = 49, AccomodationId = 6, AccomodationServiceTypeId = 1, Name = "Safe" },
                new AccomodationService { AccomodationServiceId = 50, AccomodationId = 6, AccomodationServiceTypeId = 1, Name = "Elevator" },
                new AccomodationService { AccomodationServiceId = 51, AccomodationId = 6, AccomodationServiceTypeId = 1, Name = "Shops (on site) " },
                new AccomodationService { AccomodationServiceId = 52, AccomodationId = 6, AccomodationServiceTypeId = 1, Name = "Non-smoking Rooms" },
                new AccomodationService { AccomodationServiceId = 53, AccomodationId = 6, AccomodationServiceTypeId = 1, Name = "Heating" },


                new AccomodationService { AccomodationServiceId = 54, AccomodationId = 6, AccomodationServiceTypeId = 2, Name = "Grounds" },
                new AccomodationService { AccomodationServiceId = 55, AccomodationId = 6, AccomodationServiceTypeId = 2, Name = "Outdoor Pool" },


                new AccomodationService { AccomodationServiceId = 56, AccomodationId = 6, AccomodationServiceTypeId = 3, Name = "English" },
                new AccomodationService { AccomodationServiceId = 57, AccomodationId = 6, AccomodationServiceTypeId = 3, Name = "German" },


                new AccomodationService { AccomodationServiceId = 58, AccomodationId = 6, AccomodationServiceTypeId = 4, Name = "Pool Table" }



                 );
            context.AccomodationFacilities.AddOrUpdate(
                x => x.AccomodationFacilityId,
                //First 
                new AccomodationFacility { AccomodationFacilityId = 1, AccomodationId = 1, Description = "Best restaurant in region!", EndHours = new DateTime(2000, 1, 1, 22, 0, 0), StartHours = new DateTime(2000, 1, 1, 8, 0, 0), Name = "Restaurant Courtyard" },
                new AccomodationFacility { AccomodationFacilityId = 2, AccomodationId = 1, Description = "Carefully designed to help you reach your health and fitness goals.", EndHours = new DateTime(2000, 1, 1, 20, 0, 0), StartHours = new DateTime(2000, 1, 1, 8, 0, 0), Name = "Fitness centar \"Body Art\"" },
                new AccomodationFacility { AccomodationFacilityId = 3, AccomodationId = 1, Description = "Small but perfectly well-equipped with serene treatment rooms.", EndHours = new DateTime(2000, 1, 1, 19, 0, 0), StartHours = new DateTime(2000, 1, 1, 9, 0, 0), Name = "Maab Spa" },


                //Second
                new AccomodationFacility { AccomodationFacilityId = 4, AccomodationId = 2, Description = "This award-winning, fine dining Park Terrace Restaurant serves fresh, seasonal cuisine.", EndHours = new DateTime(2000, 1, 1, 20, 0, 0), StartHours = new DateTime(2000, 1, 1, 10, 0, 0), Name = "Park Terrace Restaurant Lounge and Bar" },
                new AccomodationFacility { AccomodationFacilityId = 5, AccomodationId = 2, Description = "It’s open 24/7 with one of the lowest membership fees.", EndHours = new DateTime(2000, 1, 1, 00, 0, 0), StartHours = new DateTime(2000, 1, 1, 8, 0, 0), Name = "The Gym London Angel" },
                new AccomodationFacility { AccomodationFacilityId = 6, AccomodationId = 2, Description = "Great treatments and striking middle-Eastern statues", EndHours = new DateTime(2000, 1, 1, 19, 0, 0), StartHours = new DateTime(2000, 1, 1, 9, 0, 0), Name = "May Fair Spa" },

                //Third
                new AccomodationFacility { AccomodationFacilityId = 7, AccomodationId = 3, Description = "Politicians, artists, writers and international superstars visited.", EndHours = new DateTime(2000, 1, 1, 22, 0, 0), StartHours = new DateTime(2000, 1, 1, 10, 0, 0), Name = "Kronenhalle Restaurant" },
                new AccomodationFacility { AccomodationFacilityId = 8, AccomodationId = 3, Description = "It has boxing room, sauna, steam room, private locker rooms.", EndHours = new DateTime(2000, 1, 1, 20, 0, 0), StartHours = new DateTime(2000, 1, 1, 8, 0, 0), Name = "CrossFit" },

                ////Fourth
                new AccomodationFacility { AccomodationFacilityId = 9, AccomodationId = 4, Description = "You can see Sarajevo from a new perspective!", EndHours = new DateTime(2000, 1, 1, 22, 0, 0), StartHours = new DateTime(2000, 1, 1, 6, 0, 0), Name = "Destination Sarajevo Guide" },

                new AccomodationFacility { AccomodationFacilityId = 10, AccomodationId = 5, Description = "It has a stylish dining room and a cool vibe.", EndHours = new DateTime(2000, 1, 1, 22, 0, 0), StartHours = new DateTime(2000, 1, 1, 8, 0, 0), Name = "The Cecil" },
                new AccomodationFacility { AccomodationFacilityId = 11, AccomodationId = 5, Description = "Club has state-of-the-art workout machines, laundry service", EndHours = new DateTime(2000, 1, 1, 20, 0, 0), StartHours = new DateTime(2000, 1, 1, 8, 0, 0), Name = "Madison Square Club" },
                new AccomodationFacility { AccomodationFacilityId = 12, AccomodationId = 5, Description = "Features custom treatments and private suites in a tranquil setting", EndHours = new DateTime(2000, 1, 1, 19, 0, 0), StartHours = new DateTime(2000, 1, 1, 9, 0, 0), Name = "Mandarin Oriental" },

                //Sixth
                new AccomodationFacility { AccomodationFacilityId = 13, AccomodationId = 6, Description = "Serves traditional food, served with finesse, and with a reasonable price tag.", EndHours = new DateTime(2000, 1, 1, 22, 0, 0), StartHours = new DateTime(2000, 1, 1, 8, 0, 0), Name = "Konoba Dalmatino" },
                new AccomodationFacility { AccomodationFacilityId = 14, AccomodationId = 6, Description = "We provide a one-stop-shop for your health and wellbeing needs.", EndHours = new DateTime(2000, 1, 1, 20, 0, 0), StartHours = new DateTime(2000, 1, 1, 8, 0, 0), Name = "Sports and Health Club" },
                new AccomodationFacility { AccomodationFacilityId = 15, AccomodationId = 6, Description = "Have hairstyle of your dreams", EndHours = new DateTime(2000, 1, 1, 17, 0, 0), StartHours = new DateTime(2000, 1, 1, 9, 0, 0), Name = "Snip Snip Hair Salon" }




                );

            context.RoomTypes.AddOrUpdate(
                x => x.RoomTypeId,
                new RoomType { RoomTypeId = 1, RoomTypeName = "Single Bedroom" },
                new RoomType { RoomTypeId = 2, RoomTypeName = "Double Bedroom" },
                new RoomType { RoomTypeId = 3, RoomTypeName = "Deluxe Suite" },
                new RoomType { RoomTypeId = 4, RoomTypeName = "Luxury King Room" },
                 new RoomType { RoomTypeId = 5, RoomTypeName = "Quadruple Room" }
                );

            context.Rooms.AddOrUpdate(
                x => x.RoomId,
                //First Hotel
                new Room { RoomId = 1, NumberOfRooms = 20, Price = 70, RoomCapacity = 1, RoomTypeId = 1, AccomodationId = 1, RoomDetails = "Safe, Air conditioning, Iron, Desk,Laptop safe, Sitting area, Heating, Shower, Hairdryer,Flat-screen TV, Wi-Fi, Telephone,Pay-per-view channels,Minibar " },
                new Room { RoomId = 2, NumberOfRooms = 14, Price = 90, RoomCapacity = 2, RoomTypeId = 2, AccomodationId = 1, RoomDetails = "Safe, Air conditioning, Desk, Heating, Hairdryer,Bathroom, Bathrobe,Slippers, Linens,Flat-screen TV, Wi-Fi, Telephone, Satellite channels ,Minibar " },
                 new Room { RoomId = 3, NumberOfRooms = 4, Price = 190, RoomCapacity = 2, RoomTypeId = 3, AccomodationId = 1, RoomDetails = " Air conditioning,  Heating, Hairdryer,Bathroom, Linens,Flat-screen TV, Wi-Fi, Telephone, Satellite channels ,Minibar " },
                //Second Hotel
                new Room { RoomId = 4, NumberOfRooms = 46, Price = 150, RoomCapacity = 1, RoomTypeId = 1, AccomodationId = 2, RoomDetails = "Safe, Air conditioning, Iron, Desk,Laptop safe, Sitting area, Heating, Shower, Hairdryer,Flat-screen TV, Wi-Fi, Telephone,Pay-per-view channels,Minibar " },
                new Room { RoomId = 5, NumberOfRooms = 15, Price = 200, RoomCapacity = 2, RoomTypeId = 2, AccomodationId = 2, RoomDetails = "Safe, Air conditioning, Desk, Heating, Hairdryer,Bathroom, Bathrobe,Slippers, Linens,Flat-screen TV, Wi-Fi, Telephone, Satellite channels ,Minibar " },
                new Room { RoomId = 6, NumberOfRooms = 10, Price = 40, RoomCapacity = 2, RoomTypeId = 3, AccomodationId = 2, RoomDetails = "Air conditioning, Desk, Heating, Bathroom, Linens,Flat-screen TV, Wi-Fi, Telephone, Satellite channels " },
                new Room { RoomId = 7, NumberOfRooms = 2, Price = 500, RoomCapacity = 2, RoomTypeId = 4, AccomodationId = 2, RoomDetails = "Safe, Air conditioning, Iron, Desk,Laptop safe, Sitting area, Heating, Shower,Flat-screen TV, Wi-Fi, Telephone,Minibar " },
                //Third Hotel
                new Room { RoomId = 8, NumberOfRooms = 20, Price = 100, RoomCapacity = 1, RoomTypeId = 1, AccomodationId = 3, RoomDetails = "Safe, Air conditioning, Iron, Desk,Laptop safe, Sitting area, Heating, Shower, Hairdryer,Flat-screen TV, Wi-Fi, Telephone,Pay-per-view channels,Minibar " },
                new Room { RoomId = 9, NumberOfRooms = 15, Price = 120, RoomCapacity = 2, RoomTypeId = 2, AccomodationId = 3, RoomDetails = "Safe, Air conditioning, Desk, Heating, Hairdryer,Bathroom, Bathrobe,Slippers, Linens,Flat-screen TV, Wi-Fi, Telephone, Satellite channels ,Minibar " },
                new Room { RoomId = 10, NumberOfRooms = 10, Price = 150, RoomCapacity = 2, RoomTypeId = 3, AccomodationId = 3, RoomDetails = "Safe, Air conditioning, Iron, Desk,Laptop safe, Hairdryer,Flat-screen TV, Wi-Fi, Telephone,Pay-per-view channels,Minibar " },
                new Room { RoomId = 11, NumberOfRooms = 1, Price = 300, RoomCapacity = 2, RoomTypeId = 4, AccomodationId = 3, RoomDetails = "Safe, Air conditioning, Desk, Heating, Hairdryer,Bathroom, Bathrobe,Slippers, Linens,Flat-screen TV, Wi-Fi, Satellite channels ,Minibar " },
                //Fourth 
                new Room { RoomId = 12, NumberOfRooms = 10, Price = 10, RoomCapacity = 2, RoomTypeId = 2, AccomodationId = 4, RoomDetails = "Air conditioning, Desk, Heating, Bathroom, Linens,Flat-screen TV, Wi-Fi, Telephone, Satellite channels " },
                 new Room { RoomId = 13, NumberOfRooms = 20, Price = 8, RoomCapacity = 4, RoomTypeId = 5, AccomodationId = 4, RoomDetails = "Air conditioning, Heating, Bathroom, Wi-Fi, Shower, Hairdryer" },
                //Fifth 
                new Room { RoomId = 14, NumberOfRooms = 15, Price = 150, RoomCapacity = 1, RoomTypeId = 1, AccomodationId = 5, RoomDetails = "Air conditioning, Desk, Heating, Bathroom, Linens,Flat-screen TV, Wi-Fi, Telephone, Satellite channels " },
                new Room { RoomId = 15, NumberOfRooms = 20, Price = 170, RoomCapacity = 2, RoomTypeId = 2, AccomodationId = 5, RoomDetails = "Safe, Air conditioning, Iron, Desk,Laptop safe, Sitting area,Telephone,Pay-per-view channels,Minibar, Heating, Shower, Hairdryer " },
                new Room { RoomId = 16, NumberOfRooms = 10, Price = 250, RoomCapacity = 2, RoomTypeId = 3, AccomodationId = 5, RoomDetails = "Air conditioning, Desk, Heating, Bathroom, Linens,Flat-screen TV, Wi-Fi, Telephone, Satellite channels " },
                new Room { RoomId = 17, NumberOfRooms = 5, Price = 400, RoomCapacity = 2, RoomTypeId = 4, AccomodationId = 5, RoomDetails = "Safe, Air conditioning, Iron, Desk,Laptop safe, Sitting area, Heating, Shower, Hairdryer,Flat-screen TV, Wi-Fi, Telephone,Pay-per-view channels,Minibar " },

                //Sixth
                new Room { RoomId = 18, NumberOfRooms = 35, Price = 170, RoomCapacity = 1, RoomTypeId = 1, AccomodationId = 6, RoomDetails = "Air conditioning, Desk, Heating, Bathroom, Linens,Flat-screen TV, Wi-Fi, Telephone, Satellite channels " },
                new Room { RoomId = 19, NumberOfRooms = 40, Price = 200, RoomCapacity = 2, RoomTypeId = 2, AccomodationId = 6, RoomDetails = "Safe, Air conditioning, Iron, Desk,Laptop safe, Sitting area,Telephone,Pay-per-view channels,Minibar, Heating, Shower, Hairdryer " },
                new Room { RoomId = 20, NumberOfRooms = 12, Price = 250, RoomCapacity = 2, RoomTypeId = 3, AccomodationId = 6, RoomDetails = "Air conditioning, Desk, Heating, Bathroom, Linens,Flat-screen TV, Wi-Fi, Telephone, Satellite channels " },
                new Room { RoomId = 21, NumberOfRooms = 5, Price = 400, RoomCapacity = 2, RoomTypeId = 4, AccomodationId = 6, RoomDetails = "Safe, Air conditioning, Iron, Desk,Laptop safe, Sitting area, Heating, Shower, Hairdryer,Flat-screen TV, Wi-Fi, Telephone,Pay-per-view channels,Minibar " }
                );





            context.Images.AddOrUpdate(
                x => x.PhotoId,
                //1
                new Photo { PhotoId = 1, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_420,w_640/v1442764162/courtyard_mliif4.jpg", AccomodationId = 1, RoomTypeId = 1, Priority = 1 },
                new Photo { PhotoId = 2, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/v1441726089/Myconian_Utopia_Resort_Mykonos-passion4luxury-9_scr3zf.jpg", AccomodationId = 1, RoomTypeId = 1, Priority = 2 },
                new Photo { PhotoId = 3, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_420,w_640/v1442929954/single_u7japx.jpg", AccomodationId = 1, RoomTypeId = 1, Priority = 3 },
                new Photo { PhotoId = 4, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_300,w_568/v1442933099/singleInside_eyvdqa.jpg", AccomodationId = 1, RoomTypeId = 1, Priority = 4 },
                new Photo { PhotoId = 5, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_420,w_640/v1442929742/twobed_o3it4r.jpg", AccomodationId = 1, RoomTypeId = 2, Priority = 3 },
                new Photo { PhotoId = 6, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_300,w_568/v1442933764/doubleInside_zwpi3f.jpg", AccomodationId = 1, RoomTypeId = 2, Priority = 4 },
                new Photo { PhotoId = 5, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_420,w_640/v1442929740/deluxe_f4oa4j.jpg", AccomodationId = 1, RoomTypeId = 3, Priority = 3 },
                new Photo { PhotoId = 6, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_300,w_568/v1442933859/deluxInside_tlrgec.jpg", AccomodationId = 1, RoomTypeId = 3, Priority = 4 },

                //2
                new Photo { PhotoId = 7, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_420,w_640/v1442764163/MS_Exterior_020_S_1024x576_jkfyly.jpg", AccomodationId = 2, RoomTypeId = 1, Priority = 1 },
                new Photo { PhotoId = 8, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/v1441726089/drz-2851-1280x550_tymanj.jpg", AccomodationId = 2, RoomTypeId = 1, Priority = 2 },
                new Photo { PhotoId = 9, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_420,w_640/v1442929954/single_u7japx.jpg", AccomodationId = 2, RoomTypeId = 1, Priority = 3 },
                new Photo { PhotoId = 10, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_300,w_568/v1442933099/singleInside_eyvdqa.jpg", AccomodationId = 2, RoomTypeId = 1, Priority = 4 },
                new Photo { PhotoId = 11, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_420,w_640/v1442929742/twobed_o3it4r.jpg", AccomodationId = 2, RoomTypeId = 2, Priority = 3 },
                new Photo { PhotoId = 12, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_300,w_568/v1442933764/doubleInside_zwpi3f.jpg", AccomodationId = 2, RoomTypeId = 2, Priority = 4 },
                new Photo { PhotoId = 13, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_420,w_640/v1442929740/deluxe_f4oa4j.jpg", AccomodationId = 2, RoomTypeId = 3, Priority = 3 },
                new Photo { PhotoId = 14, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_300,w_568/v1442933859/deluxInside_tlrgec.jpg", AccomodationId = 2, RoomTypeId = 3, Priority = 4 },
                new Photo { PhotoId = 15, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_420,w_640/v1442929743/king_auq272.jpg", AccomodationId = 2, RoomTypeId = 4, Priority = 3 },
                new Photo { PhotoId = 16, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_300,w_568/v1442933100/kingInside_fspnu3.jpg", AccomodationId = 2, RoomTypeId = 4, Priority = 4 },
                //3
                new Photo { PhotoId = 17, PhotoUrl = "  http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_420,w_640/v1442764162/maxresdefault_f6mozu.jpg", AccomodationId = 3, RoomTypeId = 1, Priority = 1 },
                new Photo { PhotoId = 18, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/v1441726089/1-Copy1-1280x550_q6bsvq.jpg", AccomodationId = 3, RoomTypeId = 1, Priority = 2 },
                new Photo { PhotoId = 19, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_420,w_640/v1442929954/single_u7japx.jpg", AccomodationId = 3, RoomTypeId = 1, Priority = 3 },
                new Photo { PhotoId = 20, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_300,w_568/v1442933099/singleInside_eyvdqa.jpg", AccomodationId = 3, RoomTypeId = 1, Priority = 4 },
                new Photo { PhotoId = 21, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_420,w_640/v1442929742/twobed_o3it4r.jpg", AccomodationId = 3, RoomTypeId = 2, Priority = 3 },
                new Photo { PhotoId = 22, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_300,w_568/v1442933764/doubleInside_zwpi3f.jpg", AccomodationId = 3, RoomTypeId = 2, Priority = 4 },
                new Photo { PhotoId = 5, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_420,w_640/v1442929740/deluxe_f4oa4j.jpg", AccomodationId = 3, RoomTypeId = 3, Priority = 3 },
                new Photo { PhotoId = 6, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_300,w_568/v1442933859/deluxInside_tlrgec.jpg", AccomodationId = 3, RoomTypeId = 3, Priority = 4 },
                 new Photo { PhotoId = 15, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_420,w_640/v1442929743/king_auq272.jpg", AccomodationId = 3, RoomTypeId = 4, Priority = 3 },
                new Photo { PhotoId = 16, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_300,w_568/v1442933100/kingInside_fspnu3.jpg", AccomodationId = 3, RoomTypeId = 4, Priority = 4 },

                //4
                new Photo { PhotoId = 23, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_420,w_640/v1442782602/hostel-vagabond-sarajevo_xjsxtb.jpg", AccomodationId = 4, RoomTypeId = 2, Priority = 1 },
                new Photo { PhotoId = 24, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/v1441726089/1-Copy1-1280x550_q6bsvq.jpg", AccomodationId = 4, RoomTypeId = 2, Priority = 2 },
                new Photo { PhotoId = 25, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_420,w_640/v1442929742/twobed_o3it4r.jpg", AccomodationId = 4, RoomTypeId = 2, Priority = 3 },
                new Photo { PhotoId = 26, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_300,w_568/v1442933764/doubleInside_zwpi3f.jpg", AccomodationId = 4, RoomTypeId = 2, Priority = 4 },
                new Photo { PhotoId = 27, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_420,w_640/v1442929756/4Bett_mo2rae.jpg", AccomodationId = 4, RoomTypeId = 5, Priority = 3 },
                new Photo { PhotoId = 28, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_300,w_568/v1442933096/4bedInside_iakf3d.jpg", AccomodationId = 4, RoomTypeId = 5, Priority = 4 },

                //5
                new Photo { PhotoId = 29, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_420,w_640/v1442929728/chelsea_j85e3o.jpg", AccomodationId = 5, RoomTypeId = 1, Priority = 1 },
                new Photo { PhotoId = 30, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/v1441726089/Myconian_Utopia_Resort_Mykonos-passion4luxury-9_scr3zf.jpg", AccomodationId = 5, RoomTypeId = 1, Priority = 2 },
                new Photo { PhotoId = 31, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_420,w_640/v1442929954/single_u7japx.jpg", AccomodationId = 5, RoomTypeId = 1, Priority = 3 },
                new Photo { PhotoId = 32, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_300,w_568/v1442933099/singleInside_eyvdqa.jpg", AccomodationId = 5, RoomTypeId = 1, Priority = 4 },
                new Photo { PhotoId = 33, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_420,w_640/v1442929742/twobed_o3it4r.jpg", AccomodationId = 5, RoomTypeId = 2, Priority = 3 },
                new Photo { PhotoId = 34, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_300,w_568/v1442933764/doubleInside_zwpi3f.jpg", AccomodationId = 5, RoomTypeId = 2, Priority = 4 },
                new Photo { PhotoId = 35, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_420,w_640/v1442929740/deluxe_f4oa4j.jpg", AccomodationId = 5, RoomTypeId = 3, Priority = 3 },
                new Photo { PhotoId = 36, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_300,w_568/v1442933859/deluxInside_tlrgec.jpg", AccomodationId = 5, RoomTypeId = 3, Priority = 4 },
                new Photo { PhotoId = 45, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_420,w_640/v1442929743/king_auq272.jpg", AccomodationId = 5, RoomTypeId = 4, Priority = 3 },
                new Photo { PhotoId = 46, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_300,w_568/v1442933100/kingInside_fspnu3.jpg", AccomodationId = 5, RoomTypeId = 4, Priority = 4 },

                 //6
                new Photo { PhotoId = 37, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_420,w_640/v1442929746/excelsior_clyh2x.jpg", AccomodationId = 6, RoomTypeId = 1, Priority = 1 },
                new Photo { PhotoId = 38, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/v1441726089/Myconian_Utopia_Resort_Mykonos-passion4luxury-9_scr3zf.jpg", AccomodationId = 6, RoomTypeId = 1, Priority = 2 },
                new Photo { PhotoId = 39, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_420,w_640/v1442929954/single_u7japx.jpg", AccomodationId = 6, RoomTypeId = 1, Priority = 3 },
                new Photo { PhotoId = 40, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_300,w_568/v1442933099/singleInside_eyvdqa.jpg", AccomodationId = 6, RoomTypeId = 1, Priority = 4 },
                new Photo { PhotoId = 41, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_420,w_640/v1442929742/twobed_o3it4r.jpg", AccomodationId = 6, RoomTypeId = 2, Priority = 3 },
                new Photo { PhotoId = 42, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_300,w_568/v1442933764/doubleInside_zwpi3f.jpg", AccomodationId = 6, RoomTypeId = 2, Priority = 4 },
                new Photo { PhotoId = 43, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_420,w_640/v1442929740/deluxe_f4oa4j.jpg", AccomodationId = 6, RoomTypeId = 3, Priority = 3 },
                new Photo { PhotoId = 44, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_300,w_568/v1442933859/deluxInside_tlrgec.jpg", AccomodationId = 6, RoomTypeId = 3, Priority = 4 },
                new Photo { PhotoId = 45, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_420,w_640/v1442929743/king_auq272.jpg", AccomodationId = 6, RoomTypeId = 4, Priority = 3 },
                new Photo { PhotoId = 46, PhotoUrl = "http://res.cloudinary.com/bitbooking/image/upload/c_scale,h_300,w_568/v1442933100/kingInside_fspnu3.jpg", AccomodationId = 6, RoomTypeId = 4, Priority = 4 }

              );

        }
    }
}

