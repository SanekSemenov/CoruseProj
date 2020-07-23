DECLARE @Good	INT = (SELECT ID FROM ObjType WHERE Code = 'GOOD')				-- �����
,@OGOOD			INT	= (SELECT ID FROM ObjType WHERE Code = 'GOOD')				-- �����
,@AutoDest		INT = (SELECT ID FROM ObjType WHERE Code = 'AUTODEST')			-- ����� ��������
,@CheckList		INT = (SELECT ID FROM ObjType WHERE Code = 'CHECKLIST')			-- ����������� ����
,@ChkZone		INT = (SELECT ID FROM ObjType WHERE Code = 'CHKZONE')			-- ������� ��������
,@Expedition	INT = (SELECT ID FROM ObjType WHERE Code = 'EXPEDITION')		-- ��������
,@Firm			INT = (SELECT ID FROM ObjType WHERE Code = 'FIRM')				-- �����
,@InvoiceOut	INT = (SELECT ID FROM ObjType WHERE Code = 'INVOICEOUT')		-- �������-������
,@QueryOut		INT = (SELECT ID FROM ObjType WHERE Code = 'QUERYOUT')			-- ������-������
,@RegQuery		INT = (SELECT ID FROM ObjType WHERE Code = 'REGQUERY')			-- ����� �� ��������
,@Series		INT = (SELECT ID FROM ObjType WHERE Code = 'SERIES')			-- ����� ������
,@Trnsprtrs		INT = (SELECT ID FROM ObjType WHERE Code = 'Trnsprtrs')			-- �����-�����������
,@Basket		INT = (SELECT ID FROM ObjType WHERE Code = 'BASKET')			-- �������
,@StuffOut		INT = (SELECT ID FROM ObjType WHERE Code = 'StuffOut')			-- ������ ���
,@AgencyExp		INT = (SELECT ID FROM ObjType WHERE Code = 'AgencyExp')			-- ��������-�����������������
,@AgencyDrv		INT = (SELECT ID FROM ObjType WHERE Code = 'AgencyDrv')			-- �������� �����������������
,@ConsExped		INT = (SELECT ID FROM ObjType WHERE Code = 'ConsExped')
,@OCAR			INT = (SELECT ID FROM ObjType WHERE Code = 'CAR')
,@OCHECKLIST	INT = (SELECT ID FROM ObjType WHERE Code = 'CHECKLIST')
,@OSERIES		INT = (SELECT ID FROM ObjType WHERE Code = 'SERIES')
,@OSERTIFICAT	INT = (SELECT ID FROM ObjType WHERE Code = 'SERTIFICAT')
,@OINVOICEOUT	INT = (SELECT ID FROM ObjType WHERE Code = 'INVOICEOUT')
--,@QueryOut	INT = (SELECT ID FROM ObjType WHERE Code = 'QUERYOUT')
,@GoodVendor	INT = (SELECT ID FROM ObjType WHERE Code = 'GoodVendor')		-- �����-�������������
--,@InvoiceOut	INT = (SELECT ID FROM ObjType WHERE Code = 'INVOICEOUT')
--,@Series		INT = (SELECT ID FROM ObjType WHERE Code = 'SERIES')
--,@Firm		INT = (SELECT ID FROM ObjType WHERE Code = 'FIRM')
,@RegSert		INT = (SELECT ID FROM ObjType WHERE Code = 'REGSERT')
,@IDDocCell		INT = (SELECT ID FROM ObjType WHERE Code = 'DocCell')
,@EmployeeID	INT = (SELECT ID FROM ObjType WHERE Code = 'EMPLOYEE')			-- ���������
,@OMAILQUERY	INT = (SELECT ID FROM ObjType WHERE Code = 'MAILQUERY')
,@OAddress		INT = (SELECT ID FROM ObjType WHERE Code = 'Address')
,@LAddress		INT = (SELECT ID FROM LinkType WHERE Code = 'Address')			--* Address(�����) -> Address(����� �� ��������) -> AUTODEST(����� ��������)
,@LCAUSE		INT = (SELECT ID FROM LinkType WHERE Code = 'CAUSE')
,@LCONTRACTOR	INT = (SELECT ID FROM LinkType WHERE Code = 'CONTRACTOR')
--,@LContractor	INT = (SELECT ID FROM LinkType WHERE Code = 'CONTRACTOR')
--,@LContractor	INT = (SELECT ID FROM LinkType WHERE Code = 'Contractor')
,@LCustomCode	INT = (SELECT ID FROM LinkType WHERE Code = 'CustomCode')		--*��� ��� CUSTOMCode(�������� ���) -> CustomCode(�������� ��� �� ������/�����) -> GOOD(�����)
,@LDestPByQO	INT = (SELECT ID FROM LinkType WHERE Code = 'DestPByQO')		--* AUTODEST(����� ��������) -> DestPByQO(����� �������� ��� ������) -> QUERYOUT(������ ������)
,@LinkDocCell	INT = (SELECT ID FROM LinkType WHERE Code = 'ExpDocCell')
,@PositionLink	INT = (SELECT ID FROM LinkType where Code = 'POSITION')
,@LBank			INT = (SELECT ID FROM LinkType WHERE Code = 'BANK')				--* BANK(����) -> BANK(����) -> TRNSPRTRS(�����-�����������)
,@LCar			INT = (SELECT ID FROM LinkType WHERE Code = 'CAR')				--*CAR(����������) -> CAR(����������) -> EXPEDITION(��������)
,@LCashQuery	INT = (SELECT ID FROM LinkType WHERE Code = 'CASHQUERY')
,@LChkQuery		INT = (SELECT ID FROM LinkType WHERE Code = 'CHKQUERY')			-- QUERYOUT(������ ������) -> CHKQUERY(����������� ����� �� ������) -> CHECKLIST(����������� ����)
,@LChkZone		INT = (SELECT ID FROM LinkType WHERE Code = 'CHKZONE')			-- CHKZONE(������� ��������) -> CHKZONE(������� �������� �� ����.�����) -> CHECKLIST(����������� ����)
,@LDriver		INT = (SELECT ID FROM LinkType WHERE Code = 'DRIVER')			--* EMPLOYEE(���������) -> DRIVER(�������� ����������) -> EXPEDITION(��������)
,@LExpDocCell	INT = (SELECT ID FROM LinkType WHERE Code = 'EXPDOCCELL')
,@LInvoiceOut	INT = (SELECT ID FROM LinkType WHERE Code = 'INVOICEOUT')		--* QUERYOUT(������ ������) -> INVOICEOUT(������� �� ������) -> INVOICEOUT(������� ������)
--,@LInvoiceOut	INT = (SELECT ID FROM LinkType WHERE Code = 'INVOICEOUT')
,@LRQTemplate	INT = (SELECT ID FROM LinkType WHERE Code = 'RQTEMPLATE')		--1 QUERYOUT(������ ������) -> RQTemplate(������-������ �� ��������) -> REGQUERY(����� �� ��������)
,@LSerLot		INT = (SELECT ID FROM LinkType WHERE Code = 'SERLOT')			--1 SERIES(����� ������) -> SERLOT(����� �� ������) -> LOT(������)
,@LTJunction	INT = (SELECT ID FROM LinkType WHERE Code = 'TJUNCTION')		--* AUTODEST(����� ��������) -> TJUNCTION(������������ ����) -> QUERYOUT(������ ������)
,@LTrnsprtrs	INT = (SELECT ID FROM LinkType WHERE Code = 'TRNSPRTRS')		--* TRNSPRTRS(�����-�����������) -> TRNSPRTRS(�����������) -> AgencyDrv(�������� �����������������)
,@LAsmQuery		INT = (SELECT ID FROM LinkType WHERE Code = 'ASMQUERY')			--1 QUERYOUT(������ ������) -> ASMQUERY(��������� ���� �� ������) -> ASMLIST(��������� ����)
,@LAsmListBsk	INT = (SELECT ID FROM LinkType WHERE Code = 'ASMLISTBSK')		--* ASMLIST(��������� ����) -> AsmListBsk(��������� ���� �� �������) -> AsmListBsk(��������� ���� �� �������)
,@LRtBskALBsk	INT = (SELECT ID FROM LinkType WHERE Code = 'RtBskALBsk')		--* BASKET(�������) -> RtBskALBsk(���������� ������� ��� ��.�� �������) -> AsmListBsk(��������� ���� �� �������)
,@LAgExpDrv		INT = (SELECT ID FROM LinkType WHERE Code = 'AgExpDrv')
,@LSerSert		INT = (SELECT ID FROM LinkType WHERE Code = 'SERSERT')			-- SERIES(����� ������) -> SERSERT(����� ������ �� �����������) -> SERTIFICAT(����������)          
,@LSertifType	INT = (SELECT ID FROM LinkType WHERE Code = 'SERTIFTYPE')		-- SERTIFTYPE(��� �����������) -> SERTIFTYPE(��� �����������) -> SERTIFICAT(����������)
--,@LSerLot		INT = (SELECT ID FROM LinkType WHERE Code = 'SERLOT')
,@L_SerVendor	INT = (SELECT ID FROM LinkType WHERE Code = 'VENDOR')			--* FIRM(�����) -> VENDOR(������������� ������) -> SERIES(����� ������)
,@L_LotProv		INT = (SELECT ID FROM LinkType WHERE Code = 'LotProv')			--FIRM(�����) -> LotProv(��������� �� ������) -> LOT(������)
,@GoodSerLink	INT = (SELECT ID FROM LinkType WHERE Code = 'GOODSER')			--GOOD(�����) -> GOODSER(����� �� ����� ������) -> SERIES(����� ������)
,@LGVGood		INT = (SELECT ID FROM LinkType WHERE Code = 'GVGOOD')			-- GOOD(�����) -> GVGood(�����) -> GoodVendor(�����-�������������)
,@LGVVendor		INT = (SELECT ID FROM LinkType WHERE Code = 'GVVENDOR')			-- FIRM(�����) -> GVvendor(�������������) -> GoodVendor(�����-�������������)
,@LRSGoodVen	INT = (SELECT ID FROM LinkType WHERE Code = 'RSGOODVEN')		-- GoodVendor(�����-�������������) -> RSGoodVen(�����-������������� ��� ��) -> RegSert(��������������� �������������)
,@LEnCode		INT = (SELECT ID FROM LinkType WHERE Code = 'ENCode')
,@ContractorLink INT = (SELECT ID FROM LinkType WHERE Code = 'CONTRACTOR')
/* -- */
DECLARE @InvInName	INT = (SELECT  ID FROM StringDesc WHERE TypeID = @OGOOD AND Code = 'InvInName') -- ����� - ������������ ��� �/�
,@EComment		INT = (SELECT ID FROM StringDesc WHERE TypeID = @Expedition AND Code = 'Comment')	-- �������� - ����������
,@DelivComm		INT = (SELECT ID FROM StringDesc WHERE TypeID = @QueryOut AND Code = 'DELIVCOMM')	-- ������ ������ - ���������� �� ��������
,@DestAddress	INT = (SELECT ID FROM StringDesc WHERE TypeID = @AutoDest AND Code = 'Address')		-- ����� �������� - �����
,@FirmAddress	INT = (SELECT ID FROM StringDesc WHERE TypeID = @Firm AND Code = 'Address')			-- �����
,@TransAddress	INT = (SELECT ID FROM StringDesc WHERE TypeID = @Trnsprtrs AND Code = 'Address')	-- �����-����������� - ����������� �����
,@TransAccount	INT = (SELECT ID FROM StringDesc WHERE TypeID = @Trnsprtrs AND Code = 'Account')	-- �����-����������� - ��������� ����
,@ADCode		INT = (SELECT ID FROM StringDesc WHERE TypeID = @AutoDest AND Code = 'ADCode')		-- ��� ������ ��������
,@Brand			INT = (SELECT ID FROM StringDesc WHERE TypeID = @OCAR AND Code = 'Brand')			-- ���������� - ����� ����������
,@RegNum		INT = (SELECT ID FROM StringDesc WHERE TypeID = @Series AND Code = 'REGNUM')		-- ����� - ���. �����
,@FirmPrvPrsName INT = (SELECT ID FROM StringDesc WHERE TypeID = @Firm AND Code = 'PrvPrsName')
,@DocNum		INT = (SELECT  ID FROM StringDesc WHERE TypeID = @RegSert AND Code = 'DOCNUM')		-- ��������������� ������������� - �����
,@IDNumberCell	INT = (SELECT ID FROM StringDesc WHERE TypeID = @IDDocCell AND Code ='Number') 
,@UserNameID	INT = (SELECT ID FROM StringDesc WHERE TypeID = @EmployeeID AND Code = 'USERNAME') 
,@COMMENTE		INT = (SELECT ID FROM StringDesc WHERE TypeID = @OMAILQUERY AND Code = 'COMMENT')
,@NUMLICENZ		INT = (SELECT ID FROM StringDesc WHERE TypeID = @FIRM AND Code = 'NUMLICENZ')
,@ShortAddr		INT = (SELECT ID FROM StringDesc WHERE TypeID = @OAddress AND Code = 'ShortAddr')	-- ����� - ������� �����
,@Comment		INT = (SELECT ID FROM StringDesc WHERE TypeID = @Firm AND Code = 'COMM')
--,@Comment		INT = (SELECT  ID FROM StringDesc WHERE TypeID = @OAddress AND Code = 'Comment')	-- ����� - �����������
,@CaseCount		INT = (SELECT ID FROM PropDesc WHERE TypeID = @CheckList AND Code = 'CASECOUNT')	-- ����������� ���� - ���������� ����
,@ColdCount		INT = (SELECT ID FROM PropDesc WHERE TypeID = @CheckList AND Code = 'COLDCOUNT')	-- ����������� ���� - ���-�� �����. ����. ����.
,@ContCount		INT = (SELECT ID FROM PropDesc WHERE TypeID = @CheckList AND Code = 'CONTCOUNT')	-- ����������� ���� - ���-�� ��������. ����. ����.
,@MaxCount		INT	= (SELECT ID FROM ProPDesc WHERE TypeID = @CheckList AND Code = 'MAXCOUNT')		-- ����������� ���� - ���-�� ����. ����.
,@ParSum		INT = (SELECT ID FROM PropDesc WHERE TypeID = @QueryOut AND Code = 'PARSUM')		-- ����� � ������
,@RQCaseCount	INT = (SELECT ID FROM PropDesc WHERE TypeID = @RegQuery AND Code = 'CASECOUNT')		-- ���-�� ����
,@RQColdCount	INT = (SELECT ID FROM PropDesc WHERE TypeID = @RegQuery AND Code = 'COLDCOUNT')		-- ���-�� ���� �����
,@RQContCount	INT = (SELECT ID FROM PropDesc WHERE TypeID = @RegQuery AND Code = 'CONTCOUNT')		-- ���-�� ���� ���������
,@RQRealWeight	INT = (SELECT ID FROM PropDesc WHERE TypeID = @RegQuery AND Code = 'REALWEIGHT')	-- ����������� ���
,@WeightChk		INT = (SELECT ID FROM PropDesc WHERE TypeID = @CheckList AND Code = 'WEIGHT')		-- ���
,@WeightSer		INT = (SELECT ID FROM PropDesc WHERE TypeID = @Series AND Code = 'WEIGHT')			-- ���
,@StWeight		INT = (SELECT ID FROM PropDesc WHERE TypeID = @StuffOut AND Code = 'Weight')		-- ������ ��� - ���
,@StMaxCount	INT = (SELECT ID FROM PropDesc WHERE TypeID = @StuffOut AND Code = 'MaxCount')		-- ���������� ����������
,@RDCaseCnt		INT = (SELECT ID FROM PropDesc WHERE TypeID = @QUERYOUT AND Code = 'RDCaseCnt')		-- ���.����� - ���������� ����
,@RDWeight		INT = (SELECT ID FROM PropDesc WHERE TypeID = @QUERYOUT AND Code = 'RDWeight')		-- ���.����� - ��� ������
,@RDCold		INT = (SELECT ID FROM PropDesc WHERE TypeID = @QUERYOUT AND Code = 'RDCold')		-- ���.����� - �����
,@RDContr		INT = (SELECT ID FROM PropDesc WHERE TypeID = @QUERYOUT AND Code = 'RDContr')		-- ���.����� - ����. � ���������
--,@CASECOUNT INT = (SELECT  ID FROM PropDesc WHERE TypeID = @OCHECKLIST AND Code = 'CASECOUNT')
,@NDS			INT = (SELECT ID FROM PropDesc WHERE TypeID = @OINVOICEOUT AND Code = 'NDS')		-- ���, %
,@MorioneCode	INT = (SELECT ID From PropDesc WHERE Code = 'MorionCode')
,@SWeight		INT = (SELECT ID FROM PropDesc WHERE TypeID = @Series AND Code = 'WEIGHT') 			--����� - ��� 
,@CalcByMax		INT = (SELECT ID FROM Status WHERE TypeID = @ChkZone AND Code = 'CALCBYMAX')		-- ������� ����� �� ����. ����.
,@Container		INT = (SELECT ID FROM Status WHERE TypeID = @Good AND Code = 'Container')			-- ��������������� � ����������
,@Control		INT = (SELECT ID FROM Status WHERE TypeID = @QueryOut AND Code = 'Control')			-- �������� �������
,@Date			INT = (SELECT ID FROM Status WHERE TypeID = @Expedition AND Code = 'Date')
,@GoodCold		INT = (SELECT ID FROM Status WHERE TypeID = @Good AND Code = 'COLD')				-- �����
,@PKKN			INT = (SELECT ID FROM Status WHERE TypeID = @Good AND Code = '����')				-- ������ ����
,@OnDepot		INT = (SELECT ID FROM Status WHERE TypeID = @QueryOut AND Code = 'ONDEPOT')			-- ���������� �� �����
,@SeasonContainer INT = (SELECT ID FROM Status WHERE TypeID = @Good AND Code = 'SeasonCont')		-- �������� ������.� ����������
,@IODate		INT = (SELECT ID FROM Status WHERE TypeID = @InvoiceOut AND Code = 'Date')
,@RegDepot		INT = (SELECT ID FROM Status WHERE TypeID = @QueryOut AND Code = 'RegDepot')		-- ������ ������ - ������ �� ��
,@FakeRegDep	INT = (SELECT ID FROM Status WHERE TypeID = @QUERYOUT AND Code = 'FakeRegDep')		-- ������ ������ - ��������� ��� ��
,@ADate			INT = (SELECT ID FROM Status WHERE TypeID = @AgencyExp AND Code = 'Date')			-- ��������-����������������� - ���� �����
,@CDate			INT = (SELECT ID FROM Status WHERE TypeID = @ConsExped AND Code = 'Date')
,@LifeTime		INT = (SELECT ID FROM Status WHERE TypeID = @OSERIES AND Code = 'LifeTime')			-- ����� - ���� ��������
,@SertDate		INT = (SELECT ID FROM Status WHERE TypeID = @OSERTIFICAT AND Code = 'SertDate')		-- ���������� - ���� ������ �����������
,@RegNumDate	INT = (SELECT ID FROM Status WHERE TypeID = @Series AND Code = 'REGNUMDATE')		-- ����� - ���� ���. ������
,@QOInternal	INT = (SELECT ID FROM Status WHERE TypeID = @QueryOut AND Code ='Internal')
,@ValIDTo		INT = (SELECT ID FROM Status WHERE TypeID = @RegSert AND Code = 'VALIDTO')			-- ��������������� ������������� - ���� �������� ��
,@ValIDFrom		INT = (SELECT ID FROM Status WHERE TypeID = @RegSert AND Code = 'VALIDFROM')		-- ��������������� ������������� - ���� �������� ��
,@SGDelivery2	INT = (SELECT ID FROM StatusGroup WHERE Code = 'DELIVERY2')							-- ������ �������� - �����
,@SGGoodMove	INT = (SELECT ID FROM StatusGroup WHERE Code = 'GoodMove')							-- ����������� �������
,@RefPriceType	INT = (SELECT ID FROM PriceType	  WHERE Code = 'Reference')							--����������� ����

