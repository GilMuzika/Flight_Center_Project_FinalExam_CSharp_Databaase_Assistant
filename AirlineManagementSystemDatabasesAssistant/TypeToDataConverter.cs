using AirlineManagementSystemDatabasesAssistant.views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystemDatabasesAssistant
{
    class TypeToDataConverter
    {
        private DAO _dao;
        const string ENCRIPTION_PHRASE = "4r8rjfnklefjkljghggGKJHnif5r5242";
        const string BASE_64_IMAGE_ABSENCE_IMAGE = "data:image/jpeg;base64,/9j/4QAYRXhpZgAASUkqAAgAAAAAAAAAAAAAAP/sABFEdWNreQABAAQAAAA8AAD/4QMpaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLwA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/PiA8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIiB4OnhtcHRrPSJBZG9iZSBYTVAgQ29yZSA1LjAtYzA2MCA2MS4xMzQ3NzcsIDIwMTAvMDIvMTItMTc6MzI6MDAgICAgICAgICI+IDxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+IDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDUzUgV2luZG93cyIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDpCRTFENTcxOTNBNTkxMUVCQjE3M0M2MzI5ODI2ODE2RSIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDpCRTFENTcxQTNBNTkxMUVCQjE3M0M2MzI5ODI2ODE2RSI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOkJFMUQ1NzE3M0E1OTExRUJCMTczQzYzMjk4MjY4MTZFIiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOkJFMUQ1NzE4M0E1OTExRUJCMTczQzYzMjk4MjY4MTZFIi8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+/+4ADkFkb2JlAGTAAAAAAf/bAIQABgQEBAUEBgUFBgkGBQYJCwgGBggL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oq76r8ZVLaU4gIJDWbvV2Y1B5ZOqzsnDdDl3hrXHKIIBw7m+g7/iSrdKOQ3iQPiSrdKOQ3iQPiSrdKOQ3iQPiSrdKOQ3iQPiSrdKOQ3iQPiSrdKOQ3iQPiSrdKOQ3iQPiSrdKOQ3iQPiSrdKOQ3iQeWdqE1Oua6YcHFgsbYAMHkQauqXaJjMGNBToCAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICDrmezxcx2JcJN3XY65dzXZq+S/f/qP4KieLucry7i73KtP257iHo4lv+D6Ku+q/GVS2lOICDxTdGkJuNz0dhdEIAtDiMA8SDp+G6T0R5buNA+G6T0R5buNA+G6T0R5buNA+G6T0R5buNA+G6T0R5buNA+G6T0R5buNA+G6T0R5buNA+G6T0R5buNA+G6T0R5buNA+G6T0R5buNA+G6T0R5buNB6JKlyUk5zpdhaXix1pJweVB60BAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBB1zPZ4uY7EuEm7rsdcu5rs1fJfv/1H8FRPF3OV5dxd7lbOqWttJpVJZAjvBiEAkAjBgX7/ABvkooYtPb7tfy1vCebgpoNPb7tfy29o1Cv0gtC+QtjKqY2jUK/SCXyEyqmNo1Cv0gl8hMqpjaNQr9IJfITKqY2jUK/SCXyEyqmNo1Cv0gl8hMqpjaNQr9IJfITKqY2jUK/SCXyEyqmNo1Cv0gl8hMqpjaNQr9IJfITKqY2jUK/SCXyEyqmNo1Cv0gl8hMqpjaNQr9IJfITKqY2jUK/SCXyEyqmNo1Cv0gl8hMqpjaNQr9IJfITKqY2jUK/SCXyEyqmNo1Cv0gl8hMqpjaNQr9IJfITKqY2jUK/SCXyEyqmNo1Cv0gl8hMqpjaNQr9IJfITKqY2jUK/SCXyEyqmNo1Cv0gl8hMqpjaNQr9IJfITKqY2jUK/SCXyEyqmNo1Cv0gl8hMqpjaNQr9IJfITKqY2jUK/SCXyEyqmNo1Cv0gl8hMqpjaNQr9IJfITKqY2jUK/SCXyEyqmNo1Cv0gl8hMqpjaNQr9IJfITKqY2jUK/SCXyEyqmNo1Cv0gl8hMqpjaNQr9IJfITKqY2jUK/SCXyEyqmNo1Cv0gl8hMqpjaNQr9IJfITKqY2jUK/SCXyEyqmNo1Cv0gl8hMqpjaNQr9IJfITKqY2jUK/SCXyEyqmNo1Cv0gl8hMqpjaNQr9IJfITKqY2jUK/SCXyEyqmNo1Cv0gl8hMqpjaNQr9IJfITKqY2jUK/SCXyEyqmNo1Cv0gl8hMqpjaNQr9IJfITKqY2jUK/SCXyEyqmNo1Cv0gl8hMqpjaNQr9IJfITKqY2jUK/SCXyEyqmNo1Cv0gl8hMqpjaNQr9IJfITKqY2jUK/SCXyEyqmNo1Cv0gl8hMqpjaNQr9IJfITKqY2jUK/SCXyEyqmNo1Cv0gl8hMqpnGJ+4dCfDczKsygRbaN8WL57vNw66ejj7/tNNrprp6/tDdfk+et55tnXudtt+pzVmVwqd/29nr++P1/j0Rn/AEx/23tOr6/x/V//2Q==";


        public TypeToDataConverter(DAO dao)
        {
            _dao = dao;
        }


        Dictionary<Type, Func<IPoco, Task<IBasicData>>> correlation = new Dictionary<Type, Func<IPoco, Task<IBasicData>>>();

        public async Task<IBasicData> ConversionSelector(IPoco selector, Type selectionType)
        {
            return await Task.Run(async() => 
            {
                if (!correlation.ContainsKey(selectionType))
                {
                    if(selector is AirlineCompany)
                        correlation.Add(selectionType, async (selectorToFunc) => { return await AirlineCompanyToData(selectorToFunc as AirlineCompany); });
                    if (selector is Country)
                        correlation.Add(selectionType, async(selectorToFunc) => { return await CountryToData(selectorToFunc as Country); });
                    if (selector is Customer)
                        correlation.Add(selectionType, async (selectorToFunc) => { return await CustomerToData(selectorToFunc as Customer); });
                    if(selector is Administrator)
                        correlation.Add(selectionType, async (selectorToFunc) => { return await AdministratorToData(selectorToFunc as Administrator); });

                    if(selector is Utility_class_User)
                    {
                        switch((selector as Utility_class_User).USER_KIND)
                        {
                            case "Administrator":
                                selectionType = typeof(Utility_class_UserAdministratorData);
                                if (!correlation.ContainsKey(selectionType))
                                    correlation.Add(selectionType, async (selectorToFunc) => { return await Task.Run(async() => 
                                    {
                                        Utility_class_UserData baseData = await Utility_class_UserToData(selectorToFunc as Utility_class_User);
                                        Utility_class_UserAdministratorData adminData = FillBasicData(new Utility_class_UserAdministratorData(), baseData) as Utility_class_UserAdministratorData;
                                        if (baseData.administrator != null)
                                        {
                                            AdministratorData administratorData = await AdministratorToData(baseData.administrator);
                                            adminData.AdministratoriD = administratorData.iD;
                                            adminData.Name = administratorData.Name;
                                        }
                                        return adminData; 
                                    }); });

                                break;
                            case "AirlineCompany":
                                selectionType = typeof(Utility_class_UserAirlineCompanyData);
                                if (!correlation.ContainsKey(selectionType))
                                    correlation.Add(selectionType, async (selectorToFunc) => { return await Task.Run(async() => 
                                    {
                                        Utility_class_UserData baseData = await Utility_class_UserToData(selectorToFunc as Utility_class_User);
                                        Utility_class_UserAirlineCompanyData airlineData = FillBasicData(new Utility_class_UserAirlineCompanyData(), baseData) as Utility_class_UserAirlineCompanyData;
                                        if (baseData.airline != null)
                                        {
                                            AirlineCompanyData airlineCompanyData = await AirlineCompanyToData(baseData.airline);
                                            airlineData.AirlineCompanyiD = airlineCompanyData.iD;
                                            airlineData.Adorning = airlineCompanyData.Adorning;
                                            airlineData.Image = airlineCompanyData.Image;
                                            airlineData.AirlineName = airlineCompanyData.AirlineName;
                                            airlineData.BaseCountryName = airlineCompanyData.BaseCountryName;
                                        }
                                        return airlineData; 
                                    }); });
                                break;
                            case "Customer":
                                selectionType = typeof(Utility_class_UserCustomerData);
                                if (!correlation.ContainsKey(selectionType))
                                    correlation.Add(selectionType, async (selectorToFunc) => { return await Task.Run(async() => 
                                    {
                                        Utility_class_UserData baseData = await Utility_class_UserToData(selectorToFunc as Utility_class_User);
                                        Utility_class_UserCustomerData customerData = FillBasicData(new Utility_class_UserCustomerData(), baseData) as Utility_class_UserCustomerData;
                                        if (baseData.customer != null)
                                        {
                                            CustomerData currentCustomerData = await CustomerToData(baseData.customer);
                                            customerData.CustomeriD = currentCustomerData.iD;
                                            customerData.FirstName = currentCustomerData.FirstName;
                                            customerData.LastName = currentCustomerData.LastName;
                                            customerData.Address = currentCustomerData.Address;
                                            customerData.Phone_Num = currentCustomerData.Phone_Num;
                                            customerData.Credit_Card = currentCustomerData.Credit_Card;
                                            customerData.Image = currentCustomerData.Image;
                                        }
                                        return customerData; 
                                    }); });
                                break;
                        }
                        
                    }
                }

                var retVal = await correlation[selectionType](selector);
                return retVal;//await correlation[selectionType](selector);
            });
            

            
        }
        private Utility_class_UserData FillBasicData(Utility_class_UserData newData, Utility_class_UserData baseData)
        {
            
            newData.iD = baseData.iD;
            newData.USER_NAME = baseData.USER_NAME;
            newData.PASSWORD = baseData.PASSWORD;
            newData.USER_KIND = baseData.USER_KIND;

            newData.AIRLINE_ID = baseData.AIRLINE_ID;
            newData.CUSTOMER_ID = baseData.CUSTOMER_ID;
            newData.ADMINISTRATOR_ID = baseData.ADMINISTRATOR_ID;

            return newData;
        }

        private async Task<CountryData> CountryToData(Country country)
        {
            return await Task.Run(() => 
            {
                CountryData data = new CountryData();
                data.iD = country.ID;
                data.Name = country.COUNTRY_NAME;
                return data;
            });
        }

        private async Task<AirlineCompanyData> AirlineCompanyToData(AirlineCompany airline)
        {
            return await Task.Run(async() => 
            {
                AirlineCompanyData data = new AirlineCompanyData();
                data.iD = airline.ID;
                data.USER_ID = airline.USER_ID;
                data.Adorning = airline.ADORNING;
                data.AirlineName = airline.AIRLINE_NAME;
                data.BaseCountryName = (await _dao.GetOneById<Country>(airline.COUNTRY_CODE)).COUNTRY_NAME;
                try
                {
                    data.Image = ImageRestorer.GetBitmapFrom64baseString(airline.IMAGE);
                }
                catch(Exception)
                {
                    data.Image = ImageRestorer.GetBitmapFrom64baseString(ImageRestorer.UnformatImage64BaseString(BASE_64_IMAGE_ABSENCE_IMAGE));
                }

                data = await UtilityClassUserTreatent(data, airline) as AirlineCompanyData;
                return data;
            });
        }

        private async Task<Utility_class_UserData> Utility_class_UserToData(Utility_class_User registeredUser)
        {
            Utility_class_UserData data = new Utility_class_UserData();
            data.iD = registeredUser.ID;
            try
            {
                data.USER_NAME = Statics.Decrypt(registeredUser.USER_NAME, ENCRIPTION_PHRASE);
            }
            catch
            {
                data.USER_NAME = registeredUser.USER_NAME;
            }
            try
            {
                data.PASSWORD = Statics.Decrypt(registeredUser.PASSWORD, ENCRIPTION_PHRASE);
            }
            catch
            {
                data.PASSWORD = registeredUser.PASSWORD;
            }
            data.USER_KIND = registeredUser.USER_KIND;

            data.ADMINISTRATOR_ID = registeredUser.ADMINISTRATOR_ID;
            data.AIRLINE_ID = registeredUser.AIRLINE_ID;
            data.CUSTOMER_ID = registeredUser.CUSTOMER_ID;

            data.customer = null;//await _dao.GetOneByRegUserId<Customer>(registeredUser.ID);
            data.airline = null;//await _dao.GetOneByRegUserId<AirlineCompany>();
            data.administrator = null;
            switch(registeredUser.USER_KIND)
            {
                case "Customer":
                    data.customer = await _dao.GetOneByRegUserId<Customer>(registeredUser.ID);
                    break;
                case "Administrator":
                    data.administrator = await _dao.GetOneByRegUserId<Administrator>(registeredUser.ID);
                    break;
                case "AirlineCompany":
                    data.airline = await _dao.GetOneByRegUserId<AirlineCompany>(registeredUser.ID);
                    break;
            }
            return data;

        }

        private async Task<CustomerData> CustomerToData(Customer customer)
        {
            CustomerData data = new CustomerData();
            data.iD = customer.ID;
            data.USER_ID = customer.USER_ID;
            data.Address = customer.ADDRESS;
            data.FirstName = customer.FIRST_NAME;
            data.LastName = customer.LAST_NAME;
            data.Phone_Num = customer.PHONE_NO;
            try
            {
                data.Credit_Card = Statics.Decrypt(customer.CREDIT_CARD_NUMBER, ENCRIPTION_PHRASE);
            }
            catch
            {
                data.Credit_Card = customer.CREDIT_CARD_NUMBER;
            }
            try
            {
                data.Image = ImageRestorer.GetBitmapFrom64baseString(customer.IMAGE);
            }
            catch (Exception)
            {
                data.Image = ImageRestorer.GetBitmapFrom64baseString(ImageRestorer.UnformatImage64BaseString(BASE_64_IMAGE_ABSENCE_IMAGE));
            }//FormatException

            data = await UtilityClassUserTreatent(data, customer) as CustomerData;
            return data;
        }


        private async Task<AdministratorData> AdministratorToData(Administrator admin)
        {
            AdministratorData data = new AdministratorData();
            data.iD = admin.ID;
            data.USER_ID = admin.USER_ID;
            data.Name = admin.NAME;

            data = await UtilityClassUserTreatent(data, admin) as AdministratorData;
            return data;
        }


        private async Task<IBasicData> UtilityClassUserTreatent(IBasicData data, IAsUserForConvertingToData item)
        {
            string absenceMessage = $"\"UtilityClassUser\" don't exist for this {item.GetType().Name}";
            data.PASSWORD = absenceMessage;
            data.USERNAME = absenceMessage;
            data.USER_KIND = absenceMessage;
            //data.USER_ID = -1;
            if (item is IAsUserForConvertingToData && item.USER_ID > 0)
            {
                Utility_class_User airlineAsUtilityClassUser = await _dao.GetOneById<Utility_class_User>(item.USER_ID);
                if (airlineAsUtilityClassUser != null)
                {
                    try
                    {
                        data.PASSWORD = Statics.Decrypt(airlineAsUtilityClassUser.PASSWORD, ENCRIPTION_PHRASE);
                    }
                    catch
                    {
                        data.PASSWORD = airlineAsUtilityClassUser.PASSWORD;
                    }
                    try
                    {
                        data.USERNAME = Statics.Decrypt(airlineAsUtilityClassUser.USER_NAME, ENCRIPTION_PHRASE);
                    }
                    catch
                    {
                        data.USERNAME = airlineAsUtilityClassUser.USER_NAME;
                    }
                    
                    data.USER_KIND = airlineAsUtilityClassUser.USER_KIND;
                    data.USER_ID = airlineAsUtilityClassUser.ID;
                }
            }
            return data;
        }

















































    }
}
