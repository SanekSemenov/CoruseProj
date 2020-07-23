DECLARE @Good	INT = (SELECT ID FROM ObjType WHERE Code = 'GOOD')				-- товар
,@OGOOD			INT	= (SELECT ID FROM ObjType WHERE Code = 'GOOD')				-- товар
,@AutoDest		INT = (SELECT ID FROM ObjType WHERE Code = 'AUTODEST')			-- пункт доставки
,@CheckList		INT = (SELECT ID FROM ObjType WHERE Code = 'CHECKLIST')			-- упаковочный лист
,@ChkZone		INT = (SELECT ID FROM ObjType WHERE Code = 'CHKZONE')			-- участок контроля
,@Expedition	INT = (SELECT ID FROM ObjType WHERE Code = 'EXPEDITION')		-- отгрузка
,@Firm			INT = (SELECT ID FROM ObjType WHERE Code = 'FIRM')				-- фирма
,@InvoiceOut	INT = (SELECT ID FROM ObjType WHERE Code = 'INVOICEOUT')		-- фактура-расход
,@QueryOut		INT = (SELECT ID FROM ObjType WHERE Code = 'QUERYOUT')			-- заявка-расход
,@RegQuery		INT = (SELECT ID FROM ObjType WHERE Code = 'REGQUERY')			-- заказ из филиалов
,@Series		INT = (SELECT ID FROM ObjType WHERE Code = 'SERIES')			-- серия товара
,@Trnsprtrs		INT = (SELECT ID FROM ObjType WHERE Code = 'Trnsprtrs')			-- фирмы-перевозчики
,@Basket		INT = (SELECT ID FROM ObjType WHERE Code = 'BASKET')			-- корзина
,@StuffOut		INT = (SELECT ID FROM ObjType WHERE Code = 'StuffOut')			-- расход ТМЦ
,@AgencyExp		INT = (SELECT ID FROM ObjType WHERE Code = 'AgencyExp')			-- отгрузка-представительство
,@AgencyDrv		INT = (SELECT ID FROM ObjType WHERE Code = 'AgencyDrv')			-- водитель представительства
,@ConsExped		INT = (SELECT ID FROM ObjType WHERE Code = 'ConsExped')
,@OCAR			INT = (SELECT ID FROM ObjType WHERE Code = 'CAR')
,@OCHECKLIST	INT = (SELECT ID FROM ObjType WHERE Code = 'CHECKLIST')
,@OSERIES		INT = (SELECT ID FROM ObjType WHERE Code = 'SERIES')
,@OSERTIFICAT	INT = (SELECT ID FROM ObjType WHERE Code = 'SERTIFICAT')
,@OINVOICEOUT	INT = (SELECT ID FROM ObjType WHERE Code = 'INVOICEOUT')
--,@QueryOut	INT = (SELECT ID FROM ObjType WHERE Code = 'QUERYOUT')
,@GoodVendor	INT = (SELECT ID FROM ObjType WHERE Code = 'GoodVendor')		-- товар-производитель
--,@InvoiceOut	INT = (SELECT ID FROM ObjType WHERE Code = 'INVOICEOUT')
--,@Series		INT = (SELECT ID FROM ObjType WHERE Code = 'SERIES')
--,@Firm		INT = (SELECT ID FROM ObjType WHERE Code = 'FIRM')
,@RegSert		INT = (SELECT ID FROM ObjType WHERE Code = 'REGSERT')
,@IDDocCell		INT = (SELECT ID FROM ObjType WHERE Code = 'DocCell')
,@EmployeeID	INT = (SELECT ID FROM ObjType WHERE Code = 'EMPLOYEE')			-- сотрудник
,@OMAILQUERY	INT = (SELECT ID FROM ObjType WHERE Code = 'MAILQUERY')
,@OAddress		INT = (SELECT ID FROM ObjType WHERE Code = 'Address')
,@LAddress		INT = (SELECT ID FROM LinkType WHERE Code = 'Address')			--* Address(Адрес) -> Address(Адрес по лицензии) -> AUTODEST(Пункт доставки)
,@LCAUSE		INT = (SELECT ID FROM LinkType WHERE Code = 'CAUSE')
,@LCONTRACTOR	INT = (SELECT ID FROM LinkType WHERE Code = 'CONTRACTOR')
--,@LContractor	INT = (SELECT ID FROM LinkType WHERE Code = 'CONTRACTOR')
--,@LContractor	INT = (SELECT ID FROM LinkType WHERE Code = 'Contractor')
,@LCustomCode	INT = (SELECT ID FROM LinkType WHERE Code = 'CustomCode')		--*УКТ ЗЕД CUSTOMCode(Товарный код) -> CustomCode(Товарный код по товару/серии) -> GOOD(Товар)
,@LDestPByQO	INT = (SELECT ID FROM LinkType WHERE Code = 'DestPByQO')		--* AUTODEST(Пункт доставки) -> DestPByQO(Пункт доставки для заявки) -> QUERYOUT(Заявка расход)
,@LinkDocCell	INT = (SELECT ID FROM LinkType WHERE Code = 'ExpDocCell')
,@PositionLink	INT = (SELECT ID FROM LinkType where Code = 'POSITION')
,@LBank			INT = (SELECT ID FROM LinkType WHERE Code = 'BANK')				--* BANK(Банк) -> BANK(Банк) -> TRNSPRTRS(Фирмы-перевозчики)
,@LCar			INT = (SELECT ID FROM LinkType WHERE Code = 'CAR')				--*CAR(Автомобиль) -> CAR(Автомобиль) -> EXPEDITION(Отгрузка)
,@LCashQuery	INT = (SELECT ID FROM LinkType WHERE Code = 'CASHQUERY')
,@LChkQuery		INT = (SELECT ID FROM LinkType WHERE Code = 'CHKQUERY')			-- QUERYOUT(Заявка расход) -> CHKQUERY(Упаковочные листы по заявке) -> CHECKLIST(Упаковочный лист)
,@LChkZone		INT = (SELECT ID FROM LinkType WHERE Code = 'CHKZONE')			-- CHKZONE(Участок контроля) -> CHKZONE(Участок контроля по упак.листу) -> CHECKLIST(Упаковочный лист)
,@LDriver		INT = (SELECT ID FROM LinkType WHERE Code = 'DRIVER')			--* EMPLOYEE(Сотрудник) -> DRIVER(водитель автомобиля) -> EXPEDITION(Отгрузка)
,@LExpDocCell	INT = (SELECT ID FROM LinkType WHERE Code = 'EXPDOCCELL')
,@LInvoiceOut	INT = (SELECT ID FROM LinkType WHERE Code = 'INVOICEOUT')		--* QUERYOUT(Заявка расход) -> INVOICEOUT(Фактура по заявке) -> INVOICEOUT(Фактура расход)
--,@LInvoiceOut	INT = (SELECT ID FROM LinkType WHERE Code = 'INVOICEOUT')
,@LRQTemplate	INT = (SELECT ID FROM LinkType WHERE Code = 'RQTEMPLATE')		--1 QUERYOUT(Заявка расход) -> RQTemplate(Заявка-шаблон по дозаказу) -> REGQUERY(Заказ из филиалов)
,@LSerLot		INT = (SELECT ID FROM LinkType WHERE Code = 'SERLOT')			--1 SERIES(Серия товара) -> SERLOT(Серия по партии) -> LOT(Партия)
,@LTJunction	INT = (SELECT ID FROM LinkType WHERE Code = 'TJUNCTION')		--* AUTODEST(Пункт доставки) -> TJUNCTION(транспортный узел) -> QUERYOUT(Заявка расход)
,@LTrnsprtrs	INT = (SELECT ID FROM LinkType WHERE Code = 'TRNSPRTRS')		--* TRNSPRTRS(Фирмы-перевозчики) -> TRNSPRTRS(Перевозчики) -> AgencyDrv(Водитель представительства)
,@LAsmQuery		INT = (SELECT ID FROM LinkType WHERE Code = 'ASMQUERY')			--1 QUERYOUT(Заявка расход) -> ASMQUERY(Сборочный лист по заявке) -> ASMLIST(Сборочный лист)
,@LAsmListBsk	INT = (SELECT ID FROM LinkType WHERE Code = 'ASMLISTBSK')		--* ASMLIST(Сборочный лист) -> AsmListBsk(Сборочный лист по корзине) -> AsmListBsk(Сборочный лист по корзине)
,@LRtBskALBsk	INT = (SELECT ID FROM LinkType WHERE Code = 'RtBskALBsk')		--* BASKET(Корзина) -> RtBskALBsk(Возвратная корзина для сб.по корзине) -> AsmListBsk(Сборочный лист по корзине)
,@LAgExpDrv		INT = (SELECT ID FROM LinkType WHERE Code = 'AgExpDrv')
,@LSerSert		INT = (SELECT ID FROM LinkType WHERE Code = 'SERSERT')			-- SERIES(Серия товара) -> SERSERT(Серия товара по сертификату) -> SERTIFICAT(Сертификат)          
,@LSertifType	INT = (SELECT ID FROM LinkType WHERE Code = 'SERTIFTYPE')		-- SERTIFTYPE(Тип сертификата) -> SERTIFTYPE(Тип сертификата) -> SERTIFICAT(Сертификат)
--,@LSerLot		INT = (SELECT ID FROM LinkType WHERE Code = 'SERLOT')
,@L_SerVendor	INT = (SELECT ID FROM LinkType WHERE Code = 'VENDOR')			--* FIRM(Фирма) -> VENDOR(производитель товара) -> SERIES(Серия товара)
,@L_LotProv		INT = (SELECT ID FROM LinkType WHERE Code = 'LotProv')			--FIRM(Фирма) -> LotProv(Поставщик по партии) -> LOT(Партия)
,@GoodSerLink	INT = (SELECT ID FROM LinkType WHERE Code = 'GOODSER')			--GOOD(Товар) -> GOODSER(Товар по серии товара) -> SERIES(Серия товара)
,@LGVGood		INT = (SELECT ID FROM LinkType WHERE Code = 'GVGOOD')			-- GOOD(Товар) -> GVGood(Товар) -> GoodVendor(Товар-производитель)
,@LGVVendor		INT = (SELECT ID FROM LinkType WHERE Code = 'GVVENDOR')			-- FIRM(Фирма) -> GVvendor(Производитель) -> GoodVendor(Товар-производитель)
,@LRSGoodVen	INT = (SELECT ID FROM LinkType WHERE Code = 'RSGOODVEN')		-- GoodVendor(Товар-производитель) -> RSGoodVen(Товар-производитель для РУ) -> RegSert(Регистрационное удостоверение)
,@LEnCode		INT = (SELECT ID FROM LinkType WHERE Code = 'ENCode')
,@ContractorLink INT = (SELECT ID FROM LinkType WHERE Code = 'CONTRACTOR')
/* -- */
DECLARE @InvInName	INT = (SELECT  ID FROM StringDesc WHERE TypeID = @OGOOD AND Code = 'InvInName') -- товар - наименование для с/ф
,@EComment		INT = (SELECT ID FROM StringDesc WHERE TypeID = @Expedition AND Code = 'Comment')	-- Отгрузка - Примечание
,@DelivComm		INT = (SELECT ID FROM StringDesc WHERE TypeID = @QueryOut AND Code = 'DELIVCOMM')	-- Заявка расход - Примечание по доставке
,@DestAddress	INT = (SELECT ID FROM StringDesc WHERE TypeID = @AutoDest AND Code = 'Address')		-- пункт доставки - адрес
,@FirmAddress	INT = (SELECT ID FROM StringDesc WHERE TypeID = @Firm AND Code = 'Address')			-- адрес
,@TransAddress	INT = (SELECT ID FROM StringDesc WHERE TypeID = @Trnsprtrs AND Code = 'Address')	-- фирмы-перевозчики - юридический адрес
,@TransAccount	INT = (SELECT ID FROM StringDesc WHERE TypeID = @Trnsprtrs AND Code = 'Account')	-- фирмы-перевозчики - расчётный счёт
,@ADCode		INT = (SELECT ID FROM StringDesc WHERE TypeID = @AutoDest AND Code = 'ADCode')		-- код пункта доставки
,@Brand			INT = (SELECT ID FROM StringDesc WHERE TypeID = @OCAR AND Code = 'Brand')			-- автомобиль - марка автомобиля
,@RegNum		INT = (SELECT ID FROM StringDesc WHERE TypeID = @Series AND Code = 'REGNUM')		-- серия - рег. номер
,@FirmPrvPrsName INT = (SELECT ID FROM StringDesc WHERE TypeID = @Firm AND Code = 'PrvPrsName')
,@DocNum		INT = (SELECT  ID FROM StringDesc WHERE TypeID = @RegSert AND Code = 'DOCNUM')		-- регистрационное удостоверение - номер
,@IDNumberCell	INT = (SELECT ID FROM StringDesc WHERE TypeID = @IDDocCell AND Code ='Number') 
,@UserNameID	INT = (SELECT ID FROM StringDesc WHERE TypeID = @EmployeeID AND Code = 'USERNAME') 
,@COMMENTE		INT = (SELECT ID FROM StringDesc WHERE TypeID = @OMAILQUERY AND Code = 'COMMENT')
,@NUMLICENZ		INT = (SELECT ID FROM StringDesc WHERE TypeID = @FIRM AND Code = 'NUMLICENZ')
,@ShortAddr		INT = (SELECT ID FROM StringDesc WHERE TypeID = @OAddress AND Code = 'ShortAddr')	-- Адрес - краткий адрес
,@Comment		INT = (SELECT ID FROM StringDesc WHERE TypeID = @Firm AND Code = 'COMM')
--,@Comment		INT = (SELECT  ID FROM StringDesc WHERE TypeID = @OAddress AND Code = 'Comment')	-- Адрес - комментарий
,@CaseCount		INT = (SELECT ID FROM PropDesc WHERE TypeID = @CheckList AND Code = 'CASECOUNT')	-- упаковочный лист - количество мест
,@ColdCount		INT = (SELECT ID FROM PropDesc WHERE TypeID = @CheckList AND Code = 'COLDCOUNT')	-- упаковочный лист - кол-во холод. макс. упак.
,@ContCount		INT = (SELECT ID FROM PropDesc WHERE TypeID = @CheckList AND Code = 'CONTCOUNT')	-- упаковочный лист - кол-во термолаб. макс. упак.
,@MaxCount		INT	= (SELECT ID FROM ProPDesc WHERE TypeID = @CheckList AND Code = 'MAXCOUNT')		-- упаковочный лист - кол-во макс. упак.
,@ParSum		INT = (SELECT ID FROM PropDesc WHERE TypeID = @QueryOut AND Code = 'PARSUM')		-- сумма к оплате
,@RQCaseCount	INT = (SELECT ID FROM PropDesc WHERE TypeID = @RegQuery AND Code = 'CASECOUNT')		-- кол-во мест
,@RQColdCount	INT = (SELECT ID FROM PropDesc WHERE TypeID = @RegQuery AND Code = 'COLDCOUNT')		-- кол-во мест холод
,@RQContCount	INT = (SELECT ID FROM PropDesc WHERE TypeID = @RegQuery AND Code = 'CONTCOUNT')		-- кол-во мест контейнер
,@RQRealWeight	INT = (SELECT ID FROM PropDesc WHERE TypeID = @RegQuery AND Code = 'REALWEIGHT')	-- фактический вес
,@WeightChk		INT = (SELECT ID FROM PropDesc WHERE TypeID = @CheckList AND Code = 'WEIGHT')		-- вес
,@WeightSer		INT = (SELECT ID FROM PropDesc WHERE TypeID = @Series AND Code = 'WEIGHT')			-- вес
,@StWeight		INT = (SELECT ID FROM PropDesc WHERE TypeID = @StuffOut AND Code = 'Weight')		-- расход ТМЦ - вес
,@StMaxCount	INT = (SELECT ID FROM PropDesc WHERE TypeID = @StuffOut AND Code = 'MaxCount')		-- количество максималок
,@RDCaseCnt		INT = (SELECT ID FROM PropDesc WHERE TypeID = @QUERYOUT AND Code = 'RDCaseCnt')		-- рег.склад - количество мест
,@RDWeight		INT = (SELECT ID FROM PropDesc WHERE TypeID = @QUERYOUT AND Code = 'RDWeight')		-- рег.склад - вес заявки
,@RDCold		INT = (SELECT ID FROM PropDesc WHERE TypeID = @QUERYOUT AND Code = 'RDCold')		-- рег.склад - холод
,@RDContr		INT = (SELECT ID FROM PropDesc WHERE TypeID = @QUERYOUT AND Code = 'RDContr')		-- рег.склад - трас. в контейнер
--,@CASECOUNT INT = (SELECT  ID FROM PropDesc WHERE TypeID = @OCHECKLIST AND Code = 'CASECOUNT')
,@NDS			INT = (SELECT ID FROM PropDesc WHERE TypeID = @OINVOICEOUT AND Code = 'NDS')		-- НДС, %
,@MorioneCode	INT = (SELECT ID From PropDesc WHERE Code = 'MorionCode')
,@SWeight		INT = (SELECT ID FROM PropDesc WHERE TypeID = @Series AND Code = 'WEIGHT') 			--серия - вес 
,@CalcByMax		INT = (SELECT ID FROM Status WHERE TypeID = @ChkZone AND Code = 'CALCBYMAX')		-- считать места по макс. упак.
,@Container		INT = (SELECT ID FROM Status WHERE TypeID = @Good AND Code = 'Container')			-- транспортировка в контейнере
,@Control		INT = (SELECT ID FROM Status WHERE TypeID = @QueryOut AND Code = 'Control')			-- контроль окончен
,@Date			INT = (SELECT ID FROM Status WHERE TypeID = @Expedition AND Code = 'Date')
,@GoodCold		INT = (SELECT ID FROM Status WHERE TypeID = @Good AND Code = 'COLD')				-- холод
,@PKKN			INT = (SELECT ID FROM Status WHERE TypeID = @Good AND Code = 'ПККН')				-- список ПККН
,@OnDepot		INT = (SELECT ID FROM Status WHERE TypeID = @QueryOut AND Code = 'ONDEPOT')			-- отправлено на склад
,@SeasonContainer INT = (SELECT ID FROM Status WHERE TypeID = @Good AND Code = 'SeasonCont')		-- сезонная трансп.в контейнере
,@IODate		INT = (SELECT ID FROM Status WHERE TypeID = @InvoiceOut AND Code = 'Date')
,@RegDepot		INT = (SELECT ID FROM Status WHERE TypeID = @QueryOut AND Code = 'RegDepot')		-- Заявка расход - Заявка на РС
,@FakeRegDep	INT = (SELECT ID FROM Status WHERE TypeID = @QUERYOUT AND Code = 'FakeRegDep')		-- Заявка расход - Фиктивная для РС
,@ADate			INT = (SELECT ID FROM Status WHERE TypeID = @AgencyExp AND Code = 'Date')			-- отгрузка-представительство - дата ввода
,@CDate			INT = (SELECT ID FROM Status WHERE TypeID = @ConsExped AND Code = 'Date')
,@LifeTime		INT = (SELECT ID FROM Status WHERE TypeID = @OSERIES AND Code = 'LifeTime')			-- серия - срок годности
,@SertDate		INT = (SELECT ID FROM Status WHERE TypeID = @OSERTIFICAT AND Code = 'SertDate')		-- сертификат - дата выдачи сертификата
,@RegNumDate	INT = (SELECT ID FROM Status WHERE TypeID = @Series AND Code = 'REGNUMDATE')		-- серия - дата рег. номера
,@QOInternal	INT = (SELECT ID FROM Status WHERE TypeID = @QueryOut AND Code ='Internal')
,@ValIDTo		INT = (SELECT ID FROM Status WHERE TypeID = @RegSert AND Code = 'VALIDTO')			-- регистрационное удостоверение - срок действия до
,@ValIDFrom		INT = (SELECT ID FROM Status WHERE TypeID = @RegSert AND Code = 'VALIDFROM')		-- регистрационное удостоверение - срок действия от
,@SGDelivery2	INT = (SELECT ID FROM StatusGroup WHERE Code = 'DELIVERY2')							-- Способ доставки - склад
,@SGGoodMove	INT = (SELECT ID FROM StatusGroup WHERE Code = 'GoodMove')							-- Особенности продажи
,@RefPriceType	INT = (SELECT ID FROM PriceType	  WHERE Code = 'Reference')							--Референтная цена

