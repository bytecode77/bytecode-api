using System.ComponentModel;

namespace BytecodeApi.Data;

/// <summary>
/// Specifies a currency (ISO 4217), where the enum name represents the 3-digit currency code and the value represents the numeric currency code.
/// </summary>
public enum Currency
{
	/// <summary>
	/// The currency is unknown.
	/// </summary>
	Unknown = 0,
	/// <summary>
	/// Lek
	/// </summary>
	[Description("Lek")]
	ALL = 8,
	/// <summary>
	/// Algerian Dinar
	/// </summary>
	[Description("د.ج")]
	DZD = 12,
	/// <summary>
	/// Argentine Peso
	/// </summary>
	[Description("$")]
	ARS = 32,
	/// <summary>
	/// Australian Dollar
	/// </summary>
	[Description("$")]
	AUD = 36,
	/// <summary>
	/// Bahamian Dollar
	/// </summary>
	[Description("$")]
	BSD = 44,
	/// <summary>
	/// Bahraini Dinar
	/// </summary>
	[Description("ب.د")]
	BHD = 48,
	/// <summary>
	/// Taka
	/// </summary>
	[Description("৳")]
	BDT = 50,
	/// <summary>
	/// Armenian Dram
	/// </summary>
	[Description("Դ")]
	AMD = 51,
	/// <summary>
	/// Barbados Dollar
	/// </summary>
	[Description("$")]
	BBD = 52,
	/// <summary>
	/// Bermudian Dollar
	/// </summary>
	[Description("$")]
	BMD = 60,
	/// <summary>
	/// Ngultrum
	/// </summary>
	BTN = 64,
	/// <summary>
	/// Boliviano
	/// </summary>
	[Description("Bs.")]
	BOB = 68,
	/// <summary>
	/// Pula
	/// </summary>
	[Description("P")]
	BWP = 72,
	/// <summary>
	/// Belize Dollar
	/// </summary>
	[Description("$")]
	BZD = 84,
	/// <summary>
	/// Solomon Islands Dollar
	/// </summary>
	[Description("$")]
	SBD = 90,
	/// <summary>
	/// Brunei Dollar
	/// </summary>
	[Description("$")]
	BND = 96,
	/// <summary>
	/// Kyat
	/// </summary>
	[Description("K")]
	MMK = 104,
	/// <summary>
	/// Burundi Franc
	/// </summary>
	[Description("₣")]
	BIF = 108,
	/// <summary>
	/// Riel
	/// </summary>
	[Description("៛")]
	KHR = 116,
	/// <summary>
	/// Canadian Dollar
	/// </summary>
	[Description("$")]
	CAD = 124,
	/// <summary>
	/// Cabo Verde Escudo
	/// </summary>
	CVE = 132,
	/// <summary>
	/// Cayman Islands Dollar
	/// </summary>
	[Description("$")]
	KYD = 136,
	/// <summary>
	/// Sri Lanka Rupee
	/// </summary>
	[Description("Rs")]
	LKR = 144,
	/// <summary>
	/// Chilean Peso
	/// </summary>
	[Description("$")]
	CLP = 152,
	/// <summary>
	/// Yuan Renminbi
	/// </summary>
	[Description("¥")]
	CNY = 156,
	/// <summary>
	/// Colombian Peso
	/// </summary>
	[Description("$")]
	COP = 170,
	/// <summary>
	/// Comoro Franc
	/// </summary>
	KMF = 174,
	/// <summary>
	/// Costa Rican Colon
	/// </summary>
	[Description("₡")]
	CRC = 188,
	/// <summary>
	/// Kuna
	/// </summary>
	[Description("Kn")]
	HRK = 191,
	/// <summary>
	/// Cuban Peso
	/// </summary>
	CUP = 192,
	/// <summary>
	/// Czech Koruna
	/// </summary>
	[Description("Kč")]
	CZK = 203,
	/// <summary>
	/// Danish Krone
	/// </summary>
	[Description("kr")]
	DKK = 208,
	/// <summary>
	/// Dominican Peso
	/// </summary>
	[Description("$")]
	DOP = 214,
	/// <summary>
	/// El Salvador Colon
	/// </summary>
	SVC = 222,
	/// <summary>
	/// Ethiopian Birr
	/// </summary>
	ETB = 230,
	/// <summary>
	/// Nakfa
	/// </summary>
	[Description("Nfk")]
	ERN = 232,
	/// <summary>
	/// Falkland Islands Pound
	/// </summary>
	[Description("£")]
	FKP = 238,
	/// <summary>
	/// Fiji Dollar
	/// </summary>
	[Description("$")]
	FJD = 242,
	/// <summary>
	/// Djibouti Franc
	/// </summary>
	[Description("₣")]
	DJF = 262,
	/// <summary>
	/// Dalasi
	/// </summary>
	[Description("D")]
	GMD = 270,
	/// <summary>
	/// Gibraltar Pound
	/// </summary>
	[Description("£")]
	GIP = 292,
	/// <summary>
	/// Quetzal
	/// </summary>
	[Description("Q")]
	GTQ = 320,
	/// <summary>
	/// Guinea Franc
	/// </summary>
	[Description("₣")]
	GNF = 324,
	/// <summary>
	/// Guyana Dollar
	/// </summary>
	[Description("$")]
	GYD = 328,
	/// <summary>
	/// Gourde
	/// </summary>
	[Description("G")]
	HTG = 332,
	/// <summary>
	/// Lempira
	/// </summary>
	[Description("L")]
	HNL = 340,
	/// <summary>
	/// Hong Kong Dollar
	/// </summary>
	[Description("$")]
	HKD = 344,
	/// <summary>
	/// Forint
	/// </summary>
	[Description("Ft")]
	HUF = 348,
	/// <summary>
	/// Iceland Krona
	/// </summary>
	[Description("Kr")]
	ISK = 352,
	/// <summary>
	/// Indian Rupee
	/// </summary>
	[Description("₨")]
	INR = 356,
	/// <summary>
	/// Rupiah
	/// </summary>
	[Description("Rp")]
	IDR = 360,
	/// <summary>
	/// Iranian Rial
	/// </summary>
	[Description("﷼")]
	IRR = 364,
	/// <summary>
	/// Iraqi Dinar
	/// </summary>
	[Description("ع.د")]
	IQD = 368,
	/// <summary>
	/// New Israeli Sheqel
	/// </summary>
	[Description("₪")]
	ILS = 376,
	/// <summary>
	/// Jamaican Dollar
	/// </summary>
	[Description("$")]
	JMD = 388,
	/// <summary>
	/// Yen
	/// </summary>
	[Description("¥")]
	JPY = 392,
	/// <summary>
	/// Tenge
	/// </summary>
	[Description("〒")]
	KZT = 398,
	/// <summary>
	/// Jordanian Dinar
	/// </summary>
	[Description("د.ا")]
	JOD = 400,
	/// <summary>
	/// Kenyan Shilling
	/// </summary>
	[Description("Sh")]
	KES = 404,
	/// <summary>
	/// North Korean Won
	/// </summary>
	KPW = 408,
	/// <summary>
	/// Won
	/// </summary>
	KRW = 410,
	/// <summary>
	/// Kuwaiti Dinar
	/// </summary>
	[Description("د.ك")]
	KWD = 414,
	/// <summary>
	/// Som
	/// </summary>
	KGS = 417,
	/// <summary>
	/// Kip
	/// </summary>
	LAK = 418,
	/// <summary>
	/// Lebanese Pound
	/// </summary>
	[Description("ل.ل")]
	LBP = 422,
	/// <summary>
	/// Loti
	/// </summary>
	[Description("L")]
	LSL = 426,
	/// <summary>
	/// Liberian Dollar
	/// </summary>
	[Description("$")]
	LRD = 430,
	/// <summary>
	/// Libyan Dinar
	/// </summary>
	[Description("ل.د")]
	LYD = 434,
	/// <summary>
	/// Pataca
	/// </summary>
	[Description("P")]
	MOP = 446,
	/// <summary>
	/// Kwacha
	/// </summary>
	MWK = 454,
	/// <summary>
	/// Malaysian Ringgit
	/// </summary>
	[Description("RM")]
	MYR = 458,
	/// <summary>
	/// Rufiyaa
	/// </summary>
	[Description("ރ")]
	MVR = 462,
	/// <summary>
	/// Mauritius Rupee
	/// </summary>
	[Description("₨")]
	MUR = 480,
	/// <summary>
	/// Mexican Peso
	/// </summary>
	[Description("$")]
	MXN = 484,
	/// <summary>
	/// Tugrik
	/// </summary>
	[Description("₮")]
	MNT = 496,
	/// <summary>
	/// Moldovan Leu
	/// </summary>
	[Description("L")]
	MDL = 498,
	/// <summary>
	/// Moroccan Dirham
	/// </summary>
	[Description("د.م.")]
	MAD = 504,
	/// <summary>
	/// Rial Omani
	/// </summary>
	OMR = 512,
	/// <summary>
	/// Namibia Dollar
	/// </summary>
	[Description("$")]
	NAD = 516,
	/// <summary>
	/// Nepalese Rupee
	/// </summary>
	[Description("₨")]
	NPR = 524,
	/// <summary>
	/// Netherlands Antillean Guilder
	/// </summary>
	ANG = 532,
	/// <summary>
	/// Aruban Florin
	/// </summary>
	[Description("ƒ")]
	AWG = 533,
	/// <summary>
	/// Vatu
	/// </summary>
	[Description("Vt")]
	VUV = 548,
	/// <summary>
	/// New Zealand Dollar
	/// </summary>
	[Description("$")]
	NZD = 554,
	/// <summary>
	/// Cordoba Oro
	/// </summary>
	[Description("C$")]
	NIO = 558,
	/// <summary>
	/// Naira
	/// </summary>
	[Description("₦")]
	NGN = 566,
	/// <summary>
	/// Norwegian Krone
	/// </summary>
	[Description("kr")]
	NOK = 578,
	/// <summary>
	/// Pakistan Rupee
	/// </summary>
	[Description("₨")]
	PKR = 586,
	/// <summary>
	/// Balboa
	/// </summary>
	[Description("B/.")]
	PAB = 590,
	/// <summary>
	/// Kina
	/// </summary>
	[Description("K")]
	PGK = 598,
	/// <summary>
	/// Guarani
	/// </summary>
	[Description("₲")]
	PYG = 600,
	/// <summary>
	/// Nuevo Sol
	/// </summary>
	[Description("S/.")]
	PEN = 604,
	/// <summary>
	/// Philippine Peso
	/// </summary>
	[Description("₱")]
	PHP = 608,
	/// <summary>
	/// Qatari Rial
	/// </summary>
	[Description("ر.ق")]
	QAR = 634,
	/// <summary>
	/// Russian Ruble
	/// </summary>
	[Description("₽")]
	RUB = 643,
	/// <summary>
	/// Rwanda Franc
	/// </summary>
	[Description("₣")]
	RWF = 646,
	/// <summary>
	/// Saint Helena Pound
	/// </summary>
	[Description("£")]
	SHP = 654,
	/// <summary>
	/// Saudi Riyal
	/// </summary>
	[Description("ر.س")]
	SAR = 682,
	/// <summary>
	/// Seychelles Rupee
	/// </summary>
	SCR = 690,
	/// <summary>
	/// Singapore Dollar
	/// </summary>
	SGD = 702,
	/// <summary>
	/// Dong
	/// </summary>
	[Description("₫")]
	VND = 704,
	/// <summary>
	/// Somali Shilling
	/// </summary>
	[Description("Sh")]
	SOS = 706,
	/// <summary>
	/// Rand
	/// </summary>
	[Description("R")]
	ZAR = 710,
	/// <summary>
	/// South Sudanese Pound
	/// </summary>
	SSP = 728,
	/// <summary>
	/// Lilangeni
	/// </summary>
	[Description("L")]
	SZL = 748,
	/// <summary>
	/// Swedish Krona
	/// </summary>
	[Description("kr")]
	SEK = 752,
	/// <summary>
	/// Swiss Franc
	/// </summary>
	[Description("₣")]
	CHF = 756,
	/// <summary>
	/// Syrian Pound
	/// </summary>
	SYP = 760,
	/// <summary>
	/// Baht
	/// </summary>
	[Description("฿")]
	THB = 764,
	/// <summary>
	/// Pa’anga
	/// </summary>
	[Description("T$")]
	TOP = 776,
	/// <summary>
	/// Trinidad and Tobago Dollar
	/// </summary>
	[Description("$")]
	TTD = 780,
	/// <summary>
	/// UAE Dirham
	/// </summary>
	AED = 784,
	/// <summary>
	/// Tunisian Dinar
	/// </summary>
	[Description("د.ت")]
	TND = 788,
	/// <summary>
	/// Uganda Shilling
	/// </summary>
	[Description("Sh")]
	UGX = 800,
	/// <summary>
	/// Denar
	/// </summary>
	MKD = 807,
	/// <summary>
	/// Egyptian Pound
	/// </summary>
	[Description("£")]
	EGP = 818,
	/// <summary>
	/// Pound Sterling
	/// </summary>
	[Description("£")]
	GBP = 826,
	/// <summary>
	/// Tanzanian Shilling
	/// </summary>
	[Description("Sh")]
	TZS = 834,
	/// <summary>
	/// US Dollar
	/// </summary>
	[Description("$")]
	USD = 840,
	/// <summary>
	/// Peso Uruguayo
	/// </summary>
	[Description("$")]
	UYU = 858,
	/// <summary>
	/// Uzbekistan Sum
	/// </summary>
	UZS = 860,
	/// <summary>
	/// Tala
	/// </summary>
	WST = 882,
	/// <summary>
	/// Yemeni Rial
	/// </summary>
	[Description("﷼")]
	YER = 886,
	/// <summary>
	/// New Taiwan Dollar
	/// </summary>
	[Description("NT$")]
	TWD = 901,
	/// <summary>
	/// Leone
	/// </summary>
	SLE = 925,
	/// <summary>
	/// Bolivar
	/// </summary>
	VED = 926,
	/// <summary>
	/// Ouguiya
	/// </summary>
	[Description("UM")]
	MRU = 929,
	/// <summary>
	/// Dobra
	/// </summary>
	[Description("Db")]
	STN = 930,
	/// <summary>
	/// Peso Convertible
	/// </summary>
	CUC = 931,
	/// <summary>
	/// Zimbabwe Dollar
	/// </summary>
	ZWL = 932,
	/// <summary>
	/// Belarussian Ruble
	/// </summary>
	[Description("p.")]
	BYN = 933,
	/// <summary>
	/// Turkmenistan New Manat
	/// </summary>
	[Description("m")]
	TMT = 934,
	/// <summary>
	/// Ghana Cedi
	/// </summary>
	[Description("₵")]
	GHS = 936,
	/// <summary>
	/// Bolivar
	/// </summary>
	VEF = 937,
	/// <summary>
	/// Sudanese Pound
	/// </summary>
	[Description("£")]
	SDG = 938,
	/// <summary>
	/// Uruguay Peso en Unidades Indexadas (URUIURUI)
	/// </summary>
	UYI = 940,
	/// <summary>
	/// Serbian Dinar
	/// </summary>
	[Description("din")]
	RSD = 941,
	/// <summary>
	/// Mozambique Metical
	/// </summary>
	[Description("MTn")]
	MZN = 943,
	/// <summary>
	/// Azerbaijanian Manat
	/// </summary>
	[Description("₼")]
	AZN = 944,
	/// <summary>
	/// Romanian Leu
	/// </summary>
	[Description("L")]
	RON = 946,
	/// <summary>
	/// WIR Euro
	/// </summary>
	CHE = 947,
	/// <summary>
	/// WIR Franc
	/// </summary>
	CHW = 948,
	/// <summary>
	/// Turkish Lira
	/// </summary>
	TRY = 949,
	/// <summary>
	/// CFA Franc BEAC
	/// </summary>
	[Description("₣")]
	XAF = 950,
	/// <summary>
	/// East Caribbean Dollar
	/// </summary>
	[Description("$")]
	XCD = 951,
	/// <summary>
	/// CFA Franc BCEAO
	/// </summary>
	XOF = 952,
	/// <summary>
	/// CFP Franc
	/// </summary>
	[Description("₣")]
	XPF = 953,
	/// <summary>
	/// SDR (Special Drawing Right)
	/// </summary>
	XDR = 960,
	/// <summary>
	/// ADB Unit of Account
	/// </summary>
	XUA = 965,
	/// <summary>
	/// Zambian Kwacha
	/// </summary>
	[Description("ZK")]
	ZMW = 967,
	/// <summary>
	/// Surinam Dollar
	/// </summary>
	[Description("$")]
	SRD = 968,
	/// <summary>
	/// Malagasy Ariary
	/// </summary>
	[Description("MK")]
	MGA = 969,
	/// <summary>
	/// Unidad de Valor Real
	/// </summary>
	COU = 970,
	/// <summary>
	/// Afghani
	/// </summary>
	[Description("؋")]
	AFN = 971,
	/// <summary>
	/// Somoni
	/// </summary>
	[Description("ЅМ")]
	TJS = 972,
	/// <summary>
	/// Kwanza
	/// </summary>
	[Description("is")]
	AOA = 973,
	/// <summary>
	/// Bulgarian Lev
	/// </summary>
	[Description("лв")]
	BGN = 975,
	/// <summary>
	/// Congolese Franc
	/// </summary>
	[Description("₣")]
	CDF = 976,
	/// <summary>
	/// Convertible Mark
	/// </summary>
	[Description("КМ")]
	BAM = 977,
	/// <summary>
	/// Euro
	/// </summary>
	[Description("€")]
	EUR = 978,
	/// <summary>
	/// Mexican Unidad de Inversion (UDI)
	/// </summary>
	MXV = 979,
	/// <summary>
	/// Hryvnia
	/// </summary>
	[Description("₴")]
	UAH = 980,
	/// <summary>
	/// Lari
	/// </summary>
	[Description("ლ")]
	GEL = 981,
	/// <summary>
	/// Mvdol
	/// </summary>
	[Description("Mvdol")]
	BOV = 984,
	/// <summary>
	/// Zloty
	/// </summary>
	[Description("zł")]
	PLN = 985,
	/// <summary>
	/// Brazilian Real
	/// </summary>
	[Description("R$")]
	BRL = 986,
	/// <summary>
	/// Unidad de Fomento
	/// </summary>
	CLF = 990,
	/// <summary>
	/// Sucre
	/// </summary>
	XSU = 994,
	/// <summary>
	/// US Dollar (Next day)
	/// </summary>
	USN = 997
}