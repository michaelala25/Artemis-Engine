// AUTHOR: Miroslav Stampar
// With minor modifications and additions from Michael Ala

/*
**************************************************************************
**
**    Class  SpecialFunction (C#)
**
**************************************************************************
**    Copyright (C) 1984 Stephen L. Moshier (original C version - Cephes Math Library)
**    Copyright (C) 1996 Leigh Brookshaw	(Java version)
**    Copyright (C) 2005 Miroslav Stampar	(C# version [->this<-])
**
**    This program is free software; you can redistribute it and/or modify
**    it under the terms of the GNU General Public License as published by
**    the Free Software Foundation; either version 2 of the License, or
**    (at your option) any later version.
**
**    This program is distributed in the hope that it will be useful,
**    but WITHOUT ANY WARRANTY; without even the implied warranty of
**    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
**    GNU General Public License for more details.
**
**    You should have received a copy of the GNU General Public License
**    along with this program; if not, write to the Free Software
**    Foundation, Inc., 675 Mass Ave, Cambridge, MA 02139, USA.
**************************************************************************
**
**    This class is an extension of Math. It includes a number
**    of special functions not found in the Math class.
**
*************************************************************************/


/*
* This class contains physical constants and special functions not found
* in the Math class.
* Like the Math class this class is final and cannot be
* subclassed.
* All physical constants are in cgs units.
* NOTE: These special functions do not necessarily use the fastest
* or most accurate algorithms.
*
* @version $Revision: 1.8 $, $Date: 2005/09/12 09:52:34 $
*/

using System;

namespace Artemis.Engine.Maths
{

	public static class SpecialFunctions
	{
		// Machine constants

		private const double MACHEP = 1.11022302462515654042E-16;
		private const double MAXLOG = 7.09782712893383996732E2;
		private const double MINLOG = -7.451332191019412076235E2;
		private const double MAXGAM = 171.624376956302725;
		private const double SQTPI = 2.50662827463100050242E0;
		private const double SQRTH = 7.07106781186547524401E-1;
		private const double LOGPI = 1.14472988584940017414;
        private const double MAXAIRY = 25.77;
        private const double SQPII = 5.64189583547756286948E-1;
        private const double AIRY_C1 = 0.35502805388781723926;
        private const double AIRY_C2 = 0.258819403792806798405;
        private const double SQRT3 = 1.732050807568877293527;
        private const double LN2 = 0.6931471805599453;


		// Physical Constants in cgs Units

		/// <summary>
		/// Boltzman Constant. Units erg/deg(K) 
		/// </summary>
		public const double BOLTZMAN = 1.3807e-16;

		/// <summary>
		/// Elementary Charge. Units statcoulomb 
		/// </summary>
		public const double ECHARGE = 4.8032e-10;

		/// <summary>
		/// Electron Mass. Units g 
		/// </summary>
		public const double EMASS = 9.1095e-28;

		/// <summary>
		/// Proton Mass. Units g 
		/// </summary>
		public const double PMASS = 1.6726e-24;

		/// <summary>
		/// Gravitational Constant. Units dyne-cm^2/g^2
		/// </summary>
		public const double GRAV = 6.6720e-08;

		/// <summary>
		/// Planck constant. Units erg-sec 
		/// </summary>
		public const double PLANCK = 6.6262e-27;

		/// <summary>
		/// Speed of Light in a Vacuum. Units cm/sec 
		/// </summary>
		public const double LIGHTSPEED = 2.9979e10;

		/// <summary>
		/// Stefan-Boltzman Constant. Units erg/cm^2-sec-deg^4 
		/// </summary>
		public const double STEFANBOLTZ = 5.6703e-5;

		/// <summary>
		/// Avogadro Number. Units  1/mol 
		/// </summary>
		public const double AVOGADRO = 6.0220e23;

		/// <summary>
		/// Gas Constant. Units erg/deg-mol 
		/// </summary>
		public const double GASCONSTANT = 8.3144e07;

		/// <summary>
		/// Gravitational Acceleration at the Earths surface. Units cm/sec^2 
		/// </summary>
		public const double GRAVACC = 980.67;

		/// <summary>
		/// Solar Mass. Units g 
		/// </summary>
		public const double SOLARMASS = 1.99e33;

		/// <summary>
		/// Solar Radius. Units cm
		/// </summary>
		public const double SOLARRADIUS = 6.96e10;

		/// <summary>
		/// Solar Luminosity. Units erg/sec
		/// </summary>
		public const double SOLARLUM = 3.90e33;

		/// <summary>
		/// Solar Flux. Units erg/cm^2-sec
		/// </summary>
		public const double SOLARFLUX = 6.41e10;

		/// <summary>
		/// Astronomical Unit (radius of the Earth's orbit). Units cm
		/// </summary>
		public const double AU = 1.50e13;

        /// <summary>
        /// The Euler-Mascheroni constant.
        /// </summary>
        public const double GAMMA = 0.5772156649015329;

        private static double[] GAMMA_P = {
							 1.60119522476751861407E-4,
							 1.19135147006586384913E-3,
							 1.04213797561761569935E-2,
							 4.76367800457137231464E-2,
							 2.07448227648435975150E-1,
							 4.94214826801497100753E-1,
							 9.99999999999999996796E-1
						 };

        private static double[] GAMMA_Q = {
							 -2.31581873324120129819E-5,
							 5.39605580493303397842E-4,
							 -4.45641913851797240494E-3,
							 1.18139785222060435552E-2,
							 3.58236398605498653373E-2,
							 -2.34591795718243348568E-1,
							 7.14304917030273074085E-2,
							 1.00000000000000000320E0
						 };

        private static double[] STIR = {
								7.87311395793093628397E-4,
								-2.29549961613378126380E-4,
								-2.68132617805781232825E-3,
								3.47222221605458667310E-3,
								8.33333333333482257126E-2,
			};

        private static double[] ERFC_P = {
							 2.46196981473530512524E-10,
							 5.64189564831068821977E-1,
							 7.46321056442269912687E0,
							 4.86371970985681366614E1,
							 1.96520832956077098242E2,
							 5.26445194995477358631E2,
							 9.34528527171957607540E2,
							 1.02755188689515710272E3,
							 5.57535335369399327526E2
						 };

        private static double[] ERFC_Q = {
							 //1.0
							 1.32281951154744992508E1,
							 8.67072140885989742329E1,
							 3.54937778887819891062E2,
							 9.75708501743205489753E2,
							 1.82390916687909736289E3,
							 2.24633760818710981792E3,
							 1.65666309194161350182E3,
							 5.57535340817727675546E2
						 };

        private static double[] ERFC_R = {
							 5.64189583547755073984E-1,
							 1.27536670759978104416E0,
							 5.01905042251180477414E0,
							 6.16021097993053585195E0,
							 7.40974269950448939160E0,
							 2.97886665372100240670E0
						 };

        private static double[] ERC_S = {
							 //1.00000000000000000000E0, 
							 2.26052863220117276590E0,
							 9.39603524938001434673E0,
							 1.20489539808096656605E1,
							 1.70814450747565897222E1,
							 9.60896809063285878198E0,
							 3.36907645100081516050E0
						 };

        private static double[] ERF_T = {
							 9.60497373987051638749E0,
							 9.00260197203842689217E1,
							 2.23200534594684319226E3,
							 7.00332514112805075473E3,
							 5.55923013010394962768E4
						 };

        private static double[] ERF_U = {
							 //1.00000000000000000000E0,
							 3.35617141647503099647E1,
							 5.21357949780152679795E2,
							 4.59432382970980127987E3,
							 2.26290000613890934246E4,
							 4.92673942608635921086E4
						 };

        private static double[] LGAMMA_A = {
							 8.11614167470508450300E-4,
							 -5.95061904284301438324E-4,
							 7.93650340457716943945E-4,
							 -2.77777777730099687205E-3,
							 8.33333333333331927722E-2
						 };

        private static double[] LGAMMA_B = {
							 -1.37825152569120859100E3,
							 -3.88016315134637840924E4,
							 -3.31612992738871184744E5,
							 -1.16237097492762307383E6,
							 -1.72173700820839662146E6,
							 -8.53555664245765465627E5
						 };

        private static double[] LGAMMA_C = {
							 /* 1.00000000000000000000E0, */
							 -3.51815701436523470549E2,
							 -1.70642106651881159223E4,
							 -2.20528590553854454839E5,
							 -1.13933444367982507207E6,
							 -2.53252307177582951285E6,
							 -2.01889141433532773231E6
						 };

        private static double[] AFN = {
                            -1.31696323418331795333E-1,
                            -6.26456544431912369773E-1,
                            -6.93158036036933542233E-1,
                            -2.79779981545119124951E-1,
                            -4.91900132609500318020E-2,
                            -4.06265923594885404393E-3,
                            -1.59276496239262096340E-4,
                            -2.77649108155232920844E-6,
                            -1.67787698489114633780E-8,
                        };

        private static double[] AFD = {
                            /* 1.00000000000000000000E0, */
                            1.33560420706553243746E1,
                            3.26825032795224613948E1,
                            2.67367040941499554804E1,
                            9.18707402907259625840E0,
                            1.47529146771666414581E0,
                            1.15687173795188044134E-1,
                            4.40291641615211203805E-3,
                            7.54720348287414296618E-5,
                            4.51850092970580378464E-7,
                        };

        private static double[] AGN = {
                            1.97339932091685679179E-2,
                            3.91103029615688277255E-1,
                            1.06579897599595591108E0,
                            9.39169229816650230044E-1,
                            3.51465656105547619242E-1,
                            6.33888919628925490927E-2,
                            5.85804113048388458567E-3,
                            2.82851600836737019778E-4,
                            6.98793669997260967291E-6,
                            8.11789239554389293311E-8,
                            3.41551784765923618484E-10,
                        };

        private static double[] AGD = {
                            /*  1.00000000000000000000E0, */
                            9.30892908077441974853E0,
                            1.98352928718312140417E1,
                            1.55646628932864612953E1,
                            5.47686069422975497931E0,
                            9.54293611618961883998E-1,
                            8.64580826352392193095E-2,
                            4.12656523824222607191E-3,
                            1.01259085116509135510E-4,
                            1.17166733214413521882E-6,
                            4.91834570062930015649E-9,
                        };
        private static double[] AN = {
                            3.46538101525629032477E-1,
                            1.20075952739645805542E1,
                            7.62796053615234516538E1,
                            1.68089224934630576269E2,
                            1.59756391350164413639E2,
                            7.05360906840444183113E1,
                            1.40264691163389668864E1,
                            9.99999999999999995305E-1,
                        };

        private static double[] AD = {
                            5.67594532638770212846E-1,
                            1.47562562584847203173E1,
                            8.45138970141474626562E1,
                            1.77318088145400459522E2,
                            1.64234692871529701831E2,
                            7.14778400825575695274E1,
                            1.40959135607834029598E1,
                            1.00000000000000000470E0,
                        };

        private static double[] BN16 = {
                            -2.53240795869364152689E-1,
                            5.75285167332467384228E-1,
                            -3.29907036873225371650E-1,
                            6.44404068948199951727E-2,
                            -3.82519546641336734394E-3,
                        };

        private static double[] BD16 = {
                            /* 1.00000000000000000000E0, */
                            -7.15685095054035237902E0,
                            1.06039580715664694291E1,
                            -5.23246636471251500874E0,
                            9.57395864378383833152E-1,
                            -5.50828147163549611107E-2,
                        };

        private static double[] KNC = { 
                            .30459198558715155634315638246624251,
		                    .72037977439182833573548891941219706, 
                            -.12454959243861367729528855995001087,
		                    .27769457331927827002810119567456810e-1, 
                            -.67762371439822456447373550186163070e-2,
		                    .17238755142247705209823876688592170e-2, 
                            -.44817699064252933515310345718960928e-3,
		                    .11793660000155572716272710617753373e-3, 
                            -.31253894280980134452125172274246963e-4,
		                    .83173997012173283398932708991137488e-5, 
                            -.22191427643780045431149221890172210e-5,
		                    .59302266729329346291029599913617915e-6, 
                            -.15863051191470655433559920279603632e-6,
		                    .42459203983193603241777510648681429e-7, 
                            -.11369129616951114238848106591780146e-7,
		                    .304502217295931698401459168423403510e-8, 
                            -.81568455080753152802915013641723686e-9,
		                    .21852324749975455125936715817306383e-9, 
                            -.58546491441689515680751900276454407e-10,
		                    .15686348450871204869813586459513648e-10, 
                            -.42029496273143231373796179302482033e-11,
		                    .11261435719264907097227520956710754e-11, 
                            -.30174353636860279765375177200637590e-12,
		                    .80850955256389526647406571868193768e-13, 
                            -.21663779809421233144009565199997351e-13,
		                    .58047634271339391495076374966835526e-14, 
                            -.15553767189204733561108869588173845e-14,
		                    .41676108598040807753707828039353330e-15, 
                            -.11167065064221317094734023242188463e-15 
                        };

		// Function Methods

		/// <summary>
		/// Returns the base 10 logarithm of the specified number.
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public static double Log10(double x)
		{
			if (x <= 0.0) throw new ArithmeticException("range exception");
			return Math.Log(x)/2.30258509299404568401;
		}
			
		/// <summary>
		/// Returns the hyperbolic arc cosine of the specified number.
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public static double Acosh(double x)
		{
			if (x < 1.0) throw new ArithmeticException("range exception");
            return Math.Log(x + Math.Sqrt(x * x - 1));
		}
			
		/// <summary>
		/// Returns the hyperbolic arc sine of the specified number.
		/// </summary>
		/// <param name="xx"></param>
		/// <returns></returns>
		public static double Asinh(double xx)
		{
			double x;
			int sign;
			if (xx == 0.0) return xx;
			if (xx < 0.0)
			{
				sign = -1;
				x = -xx;
			}
			else
			{
				sign = 1;
				x = xx;
			}
            return sign * Math.Log(x + Math.Sqrt(x * x + 1));
		}
			
		/// <summary>
		/// Returns the hyperbolic arc tangent of the specified number.
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public static double Atanh(double x)
		{
			if (x > 1.0 || x < -1.0) throw 
										 new ArithmeticException("range exception");
            return 0.5 * Math.Log((1.0 + x) / (1.0 - x));
		}
			
		/// <summary>
		/// Returns the Bessel function of order 0 of the specified number.
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public static double J0(double x)
		{
			double ax;

            if ((ax = Math.Abs(x)) < 8.0)
			{
				double y = x*x;
				double ans1 = 57568490574.0 + y*(-13362590354.0 + y*(651619640.7
					+ y*(-11214424.18 + y*(77392.33017 + y*(-184.9052456)))));
				double ans2 = 57568490411.0 + y*(1029532985.0 + y*(9494680.718
					+ y*(59272.64853 + y*(267.8532712 + y*1.0))));

				return ans1/ans2;

			}
			else
			{
				double z = 8.0/ax;
				double y = z*z;
				double xx = ax - 0.785398164;
				double ans1 = 1.0 + y*(-0.1098628627e-2 + y*(0.2734510407e-4
					+ y*(-0.2073370639e-5 + y*0.2093887211e-6)));
				double ans2 = -0.1562499995e-1 + y*(0.1430488765e-3
					+ y*(-0.6911147651e-5 + y*(0.7621095161e-6
					- y*0.934935152e-7)));

                return Math.Sqrt(0.636619772 / ax) *
                    (Math.Cos(xx) * ans1 - z * Math.Sin(xx) * ans2);
			}
		}
			
		/// <summary>
		/// Returns the Bessel function of order 1 of the specified number.
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public static double J1(double x)
		{
			double ax;
			double y;
			double ans1, ans2;

            if ((ax = Math.Abs(x)) < 8.0)
			{
				y = x*x;
				ans1 = x*(72362614232.0 + y*(-7895059235.0 + y*(242396853.1
					+ y*(-2972611.439 + y*(15704.48260 + y*(-30.16036606))))));
				ans2 = 144725228442.0 + y*(2300535178.0 + y*(18583304.74
					+ y*(99447.43394 + y*(376.9991397 + y*1.0))));
				return ans1/ans2;
			}
			else
			{
				double z = 8.0/ax;
				double xx = ax - 2.356194491;
				y = z*z;

				ans1 = 1.0 + y*(0.183105e-2 + y*(-0.3516396496e-4
					+ y*(0.2457520174e-5 + y*(-0.240337019e-6))));
				ans2 = 0.04687499995 + y*(-0.2002690873e-3
					+ y*(0.8449199096e-5 + y*(-0.88228987e-6
					+ y*0.105787412e-6)));
                double ans = Math.Sqrt(0.636619772 / ax) *
                    (Math.Cos(xx) * ans1 - z * Math.Sin(xx) * ans2);
				if (x < 0.0) ans = -ans;
				return ans;
			}
		}
			
		/// <summary>
		/// Returns the Bessel function of order n of the specified number.
		/// </summary>
		/// <param name="n"></param>
		/// <param name="x"></param>
		/// <returns></returns>
		public static double JN(int n, double x)
		{
			int j, m;
			double ax, bj, bjm, bjp, sum, tox, ans;
			bool jsum;

			double ACC = 40.0;
			double BIGNO = 1.0e+10;
			double BIGNI = 1.0e-10;

			if (n == 0) return J0(x);
			if (n == 1) return J1(x);

            ax = Math.Abs(x);
			if (ax == 0.0) return 0.0;
			else if (ax > (double) n)
			{
				tox = 2.0/ax;
				bjm = J0(ax);
				bj = J1(ax);
				for (j = 1; j < n; j++)
				{
					bjp = j*tox*bj - bjm;
					bjm = bj;
					bj = bjp;
				}
				ans = bj;
			}
			else
			{
				tox = 2.0/ax;
                m = 2 * ((n + (int)Math.Sqrt(ACC * n)) / 2);
				jsum = false;
				bjp = ans = sum = 0.0;
				bj = 1.0;
				for (j = m; j > 0; j--)
				{
					bjm = j*tox*bj - bjp;
					bjp = bj;
					bj = bjm;
                    if (Math.Abs(bj) > BIGNO)
					{
						bj *= BIGNI;
						bjp *= BIGNI;
						ans *= BIGNI;
						sum *= BIGNI;
					}
					if (jsum) sum += bj;
					jsum = !jsum;
					if (j == n) ans = bjp;
				}
				sum = 2.0*sum - bj;
				ans /= sum;
			}
			return x < 0.0 && n%2 == 1 ? -ans : ans;
		}
			
		/// <summary>
		/// Returns the Bessel function of the second kind, of order 0 of the specified number.
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public static double Y0(double x)
		{
			if (x < 8.0)
			{
				double y = x*x;

				double ans1 = -2957821389.0 + y*(7062834065.0 + y*(-512359803.6
					+ y*(10879881.29 + y*(-86327.92757 + y*228.4622733))));
				double ans2 = 40076544269.0 + y*(745249964.8 + y*(7189466.438
					+ y*(47447.26470 + y*(226.1030244 + y*1.0))));

                return (ans1 / ans2) + 0.636619772 * J0(x) * Math.Log(x);
			}
			else
			{
				double z = 8.0/x;
				double y = z*z;
				double xx = x - 0.785398164;

				double ans1 = 1.0 + y*(-0.1098628627e-2 + y*(0.2734510407e-4
					+ y*(-0.2073370639e-5 + y*0.2093887211e-6)));
				double ans2 = -0.1562499995e-1 + y*(0.1430488765e-3
					+ y*(-0.6911147651e-5 + y*(0.7621095161e-6
					+ y*(-0.934945152e-7))));
                return Math.Sqrt(0.636619772 / x) *
                    (Math.Sin(xx) * ans1 + z * Math.Cos(xx) * ans2);
			}
		}
			
		/// <summary>
		/// Returns the Bessel function of the second kind, of order 1 of the specified number.
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public static double Y1(double x)
		{
			if (x < 8.0)
			{
				double y = x*x;
				double ans1 = x*(-0.4900604943e13 + y*(0.1275274390e13
					+ y*(-0.5153438139e11 + y*(0.7349264551e9
					+ y*(-0.4237922726e7 + y*0.8511937935e4)))));
				double ans2 = 0.2499580570e14 + y*(0.4244419664e12
					+ y*(0.3733650367e10 + y*(0.2245904002e8
					+ y*(0.1020426050e6 + y*(0.3549632885e3 + y)))));
                return (ans1 / ans2) + 0.636619772 * (J1(x) * Math.Log(x) - 1.0 / x);
			}
			else
			{
				double z = 8.0/x;
				double y = z*z;
				double xx = x - 2.356194491;
				double ans1 = 1.0 + y*(0.183105e-2 + y*(-0.3516396496e-4
					+ y*(0.2457520174e-5 + y*(-0.240337019e-6))));
				double ans2 = 0.04687499995 + y*(-0.2002690873e-3
					+ y*(0.8449199096e-5 + y*(-0.88228987e-6
					+ y*0.105787412e-6)));
                return Math.Sqrt(0.636619772 / x) *
                    (Math.Sin(xx) * ans1 + z * Math.Cos(xx) * ans2);
			}
		}
			
		/// <summary>
		/// Returns the Bessel function of the second kind, of order n of the specified number.
		/// </summary>
		/// <param name="n"></param>
		/// <param name="x"></param>
		/// <returns></returns>
		public static double YN(int n, double x)
		{
			double by, bym, byp, tox;

			if (n == 0) return Y0(x);
			if (n == 1) return Y1(x);

			tox = 2.0/x;
			by = Y1(x);
			bym = Y0(x);
			for (int j = 1; j < n; j++)
			{
				byp = j*tox*by - bym;
				bym = by;
				by = byp;
			}
			return by;
		}

		/// <summary>
		/// Returns the gamma function of the specified number.
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public static double Gamma(double x)
		{
			double p, z;

            double q = Math.Abs(x);

			if (q > 33.0)
			{
				if (x < 0.0)
				{
                    p = Math.Floor(q);
					if (p == q) throw new ArithmeticException("gamma: overflow");
					//int i = (int)p;
					z = q - p;
					if (z > 0.5)
					{
						p += 1.0;
						z = q - p;
					}
                    z = q * Math.Sin(Math.PI * z);
					if (z == 0.0) throw new ArithmeticException("gamma: overflow");
                    z = Math.Abs(z);
                    z = Math.PI / (z * Stirf(q));

					return -z;
				}
				else
				{
					return Stirf(x);
				}
			}

			z = 1.0;
			while (x >= 3.0)
			{
				x -= 1.0;
				z *= x;
			}

			while (x < 0.0)
			{
				if (x == 0.0)
				{
					throw new ArithmeticException("gamma: singular");
				}
				else if (x > -1.0E-9)
				{
                    return (z / ((1.0 + GAMMA * x) * x));
				}
				z /= x;
				x += 1.0;
			}

			while (x < 2.0)
			{
				if (x == 0.0)
				{
					throw new ArithmeticException("gamma: singular");
				}
				else if (x < 1.0E-9)
				{
                    return (z / ((1.0 + GAMMA * x) * x));
				}
				z /= x;
				x += 1.0;
			}

			if ((x == 2.0) || (x == 3.0)) return z;

			x -= 2.0;
			p = polevl(x, GAMMA_P, 6);
			q = polevl(x, GAMMA_Q, 7);
			return z*p/q;

		}
			
		/// <summary>
		/// Return the gamma function computed by Stirling's formula.
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		private static double Stirf(double x)
		{
			double MAXSTIR = 143.01608;

			double w = 1.0/x;
            double y = Math.Exp(x);

			w = 1.0 + w*polevl(w, STIR, 4);

			if (x > MAXSTIR)
			{
				/* Avoid overflow in Math.Pow() */
                double v = Math.Pow(x, 0.5 * x - 0.25);
				y = v*(v/y);
			}
			else
			{
                y = Math.Pow(x, x - 0.5) / y;
			}
			y = SQTPI*y*w;
			return y;
		}

			
		/// <summary>
		/// Returns the complemented incomplete gamma function.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="x"></param>
		/// <returns></returns>
		public static double ComplementedIncompleteGamma(double a, double x)
		{
			double big = 4.503599627370496e15;
			double biginv = 2.22044604925031308085e-16;
			double ans, ax, c, yc, r, t, y, z;
			double pk, pkm1, pkm2, qk, qkm1, qkm2;

			if (x <= 0 || a <= 0) return 1.0;

			if (x < 1.0 || x < a) return 1.0 - IncompleteGamma(a, x);

            ax = a * Math.Log(x) - x - LogGamma(a);
			if (ax < -MAXLOG) return 0.0;

            ax = Math.Exp(ax);

			/* continued fraction */
			y = 1.0 - a;
			z = x + y + 1.0;
			c = 0.0;
			pkm2 = 1.0;
			qkm2 = x;
			pkm1 = x + 1.0;
			qkm1 = z*x;
			ans = pkm1/qkm1;

			do
			{
				c += 1.0;
				y += 1.0;
				z += 2.0;
				yc = y*c;
				pk = pkm1*z - pkm2*yc;
				qk = qkm1*z - qkm2*yc;
				if (qk != 0)
				{
					r = pk/qk;
                    t = Math.Abs((ans - r) / r);
					ans = r;
				}
				else
					t = 1.0;

				pkm2 = pkm1;
				pkm1 = pk;
				qkm2 = qkm1;
				qkm1 = qk;
                if (Math.Abs(pk) > big)
				{
					pkm2 *= biginv;
					pkm1 *= biginv;
					qkm2 *= biginv;
					qkm1 *= biginv;
				}
			} while (t > MACHEP);
            
			return ans*ax;
		}


		/// <summary>
		/// Returns the incomplete gamma function.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="x"></param>
		/// <returns></returns>
		public static double IncompleteGamma(double a, double x)
		{
			double ans, ax, c, r;

			if (x <= 0 || a <= 0) return 0.0;

			if (x > 1.0 && x > a) return 1.0 - ComplementedIncompleteGamma(a, x);

			/* Compute  x**a * exp(-x) / gamma(a)  */
            ax = a * Math.Log(x) - x - LogGamma(a);
			if (ax < -MAXLOG) return (0.0);

            ax = Math.Exp(ax);

			/* power series */
			r = a;
			c = 1.0;
			ans = 1.0;

			do
			{
				r += 1.0;
				c *= x/r;
				ans += c;
			} while (c/ans > MACHEP);

			return (ans*ax/a);

		}

			
		/**
		* Returns the area under the left hand tail (from 0 to x)
		* of the Chi square probability density function with
		* v degrees of freedom.
		**/

		/// <summary>
		/// Returns the chi-square function (left hand tail).
		/// </summary>
		/// <param name="df">degrees of freedom</param>
		/// <param name="x">double value</param>
		/// <returns></returns>
		public static double ChiSqrd(double df, double x)
		{
			if (x < 0.0 || df < 1.0) return 0.0;

			return IncompleteGamma(df/2.0, x/2.0);

		}

			
		/**
		* Returns the area under the right hand tail (from x to
		* infinity) of the Chi square probability density function
		* with v degrees of freedom:
		**/

		/// <summary>
		/// Returns the chi-square function (right hand tail).
		/// </summary>
		/// <param name="df">degrees of freedom</param>
		/// <param name="x">double value</param>
		/// <returns></returns>
		public static double ComplementedChiSqrd(double df, double x)
		{
			if (x < 0.0 || df < 1.0) return 0.0;

			return ComplementedIncompleteGamma(df/2.0, x/2.0);

		}

			
		/// <summary>
		/// Returns the sum of the first k terms of the Poisson distribution.
		/// </summary>
		/// <param name="k">number of terms</param>
		/// <param name="x">double value</param>
		/// <returns></returns>
		public static double PoissonDistribution(int k, double x)
		{
			if (k < 0 || x < 0) return 0.0;

			return ComplementedIncompleteGamma((double) (k + 1), x);
		}

			
		/// <summary>
		/// Returns the sum of the terms k+1 to infinity of the Poisson distribution.
		/// </summary>
		/// <param name="k">start</param>
		/// <param name="x">double value</param>
		/// <returns></returns>
		public static double ComplementedPoissonDistribution(int k, double x)
		{
			if (k < 0 || x < 0) return 0.0;

			return IncompleteGamma((double) (k + 1), x);
		}

			
		/// <summary>
		/// Returns the area under the Gaussian probability density function, integrated from minus infinity to a.
		/// </summary>
		/// <param name="a"></param>
		/// <returns></returns>
		public static double CumulativeNormalDistribution(double a)
		{
			double x, y, z;

			x = a*SQRTH;
            z = Math.Abs(x);

			if (z < SQRTH) y = 0.5 + 0.5*Erf(x);
			else
			{
				y = 0.5*Erfc(z);
				if (x > 0) y = 1.0 - y;
			}

			return y;
		}
			
		/// <summary>
		/// Returns the complementary error function of the specified number.
		/// </summary>
		/// <param name="a"></param>
		/// <returns></returns>
		public static double Erfc(double a)
		{
			double x, y, z, p, q;

			if (a < 0.0) x = -a;
			else x = a;

			if (x < 1.0) return 1.0 - Erf(a);

			z = -a*a;

			if (z < -MAXLOG)
			{
				if (a < 0) return (2.0);
				else return (0.0);
			}

            z = Math.Exp(z);

			if (x < 8.0)
			{
				p = polevl(x, ERFC_P, 8);
				q = p1evl(x, ERFC_Q, 8);
			}
			else
			{
				p = polevl(x, ERFC_R, 5);
				q = p1evl(x, ERC_S, 6);
			}

			y = (z*p)/q;

			if (a < 0) y = 2.0 - y;

			if (y == 0.0)
			{
				if (a < 0) return 2.0;
				else return (0.0);
			}


			return y;
		}

		/// <summary>
		/// Returns the error function of the specified number.
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public static double Erf(double x)
		{
			double y, z;

            if (Math.Abs(x) > 1.0) return (1.0 - Erfc(x));
			z = x*x;
			y = x*polevl(z, ERF_T, 4)/p1evl(z, ERF_U, 5);
			return y;
		}

			
		/// <summary>
		/// Evaluates polynomial of degree N
		/// </summary>
		/// <param name="x"></param>
		/// <param name="coef"></param>
		/// <param name="N"></param>
		/// <returns></returns>
		private static double polevl(double x, double[] coef, int N)
		{
			double ans;

			ans = coef[0];

			for (int i = 1; i <= N; i++)
			{
				ans = ans*x + coef[i];
			}

			return ans;
		}

			
		/// <summary>
		/// Evaluates polynomial of degree N with assumtion that coef[N] = 1.0
		/// </summary>
		/// <param name="x"></param>
		/// <param name="coef"></param>
		/// <param name="N"></param>
		/// <returns></returns>		
		private static double p1evl(double x, double[] coef, int N)
		{
			double ans;

			ans = x + coef[0];

			for (int i = 1; i < N; i++)
			{
				ans = ans*x + coef[i];
			}

			return ans;
		}

		/// <summary>
		/// Returns the natural logarithm of gamma function.
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public static double LogGamma(double x)
		{
			double p, q, w, z;

			if (x < -34.0)
			{
				q = -x;
				w = LogGamma(q);
                p = Math.Floor(q);
				if (p == q) throw new ArithmeticException("lgam: Overflow");
				z = q - p;
				if (z > 0.5)
				{
					p += 1.0;
					z = p - q;
				}
                z = q * Math.Sin(Math.PI * z);
				if (z == 0.0) throw new
								  ArithmeticException("lgamma: Overflow");
                z = LOGPI - Math.Log(z) - w;
				return z;
			}

			if (x < 13.0)
			{
				z = 1.0;
				while (x >= 3.0)
				{
					x -= 1.0;
					z *= x;
				}
				while (x < 2.0)
				{
					if (x == 0.0) throw new
									  ArithmeticException("lgamma: Overflow");
					z /= x;
					x += 1.0;
				}
				if (z < 0.0) z = -z;
                if (x == 2.0) return Math.Log(z);
				x -= 2.0;
				p = x*polevl(x, LGAMMA_B, 5)/p1evl(x, LGAMMA_C, 6);
                return (Math.Log(z) + p);
			}

			if (x > 2.556348e305) throw new
									  ArithmeticException("lgamma: Overflow");

            q = (x - 0.5) * Math.Log(x) - x + 0.91893853320467274178;
			if (x > 1.0e8) return (q);

			p = 1.0/(x*x);
			if (x >= 1000.0)
				q += ((7.9365079365079365079365e-4*p
					- 2.7777777777777777777778e-3)*p
					+ 0.0833333333333333333333)/x;
			else
				q += polevl(p, LGAMMA_A, 4)/x;
			return q;
		}

			
		/// <summary>
		/// Returns the incomplete beta function evaluated from zero to xx.
		/// </summary>
		/// <param name="aa"></param>
		/// <param name="bb"></param>
		/// <param name="xx"></param>
		/// <returns></returns>
		public static double IncompleteBeta(double aa, double bb, double xx)
		{
			double a, b, t, x, xc, w, y;
			bool flag;

			if (aa <= 0.0 || bb <= 0.0) throw new
											ArithmeticException("ibeta: Domain error!");

			if ((xx <= 0.0) || (xx >= 1.0))
			{
				if (xx == 0.0) return 0.0;
				if (xx == 1.0) return 1.0;
				throw new ArithmeticException("ibeta: Domain error!");
			}

			flag = false;
			if ((bb*xx) <= 1.0 && xx <= 0.95)
			{
				t = pseries(aa, bb, xx);
				return t;
			}

			w = 1.0 - xx;

			/* Reverse a and b if x is greater than the mean. */
			if (xx > (aa/(aa + bb)))
			{
				flag = true;
				a = bb;
				b = aa;
				xc = xx;
				x = w;
			}
			else
			{
				a = aa;
				b = bb;
				xc = w;
				x = xx;
			}

			if (flag && (b*x) <= 1.0 && x <= 0.95)
			{
				t = pseries(a, b, x);
				if (t <= MACHEP) t = 1.0 - MACHEP;
				else t = 1.0 - t;
				return t;
			}

			/* Choose expansion for better convergence. */
			y = x*(a + b - 2.0) - (a - 1.0);
			if (y < 0.0)
				w = incbcf(a, b, x);
			else
				w = incbd(a, b, x)/xc;

			/* Multiply w by the factor
				   a      b   _             _     _
				  x  (1-x)   | (a+b) / ( a | (a) | (b) ) .   */

            y = a * Math.Log(x);
            t = b * Math.Log(xc);
            if ((a + b) < MAXGAM && Math.Abs(y) < MAXLOG && Math.Abs(t) < MAXLOG)
			{
                t = Math.Pow(xc, b);
                t *= Math.Pow(x, a);
				t /= a;
				t *= w;
				t *= Gamma(a + b)/(Gamma(a)*Gamma(b));
				if (flag)
				{
					if (t <= MACHEP) t = 1.0 - MACHEP;
					else t = 1.0 - t;
				}
				return t;
			}
			/* Resort to logarithms.  */
			y += t + LogGamma(a + b) - LogGamma(a) - LogGamma(b);
            y += Math.Log(w / a);
			if (y < MINLOG)
				t = 0.0;
			else
                t = Math.Exp(y);

			if (flag)
			{
				if (t <= MACHEP) t = 1.0 - MACHEP;
				else t = 1.0 - t;
			}
			return t;
		}

			
		/// <summary>
		/// Returns the continued fraction expansion #1 for incomplete beta integral.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <param name="x"></param>
		/// <returns></returns>
		private static double incbcf(double a, double b, double x)
		{
			double xk, pk, pkm1, pkm2, qk, qkm1, qkm2;
			double k1, k2, k3, k4, k5, k6, k7, k8;
			double r, t, ans, thresh;
			int n;
			double big = 4.503599627370496e15;
			double biginv = 2.22044604925031308085e-16;

			k1 = a;
			k2 = a + b;
			k3 = a;
			k4 = a + 1.0;
			k5 = 1.0;
			k6 = b - 1.0;
			k7 = k4;
			k8 = a + 2.0;

			pkm2 = 0.0;
			qkm2 = 1.0;
			pkm1 = 1.0;
			qkm1 = 1.0;
			ans = 1.0;
			r = 1.0;
			n = 0;
			thresh = 3.0*MACHEP;
			do
			{
				xk = -(x*k1*k2)/(k3*k4);
				pk = pkm1 + pkm2*xk;
				qk = qkm1 + qkm2*xk;
				pkm2 = pkm1;
				pkm1 = pk;
				qkm2 = qkm1;
				qkm1 = qk;

				xk = (x*k5*k6)/(k7*k8);
				pk = pkm1 + pkm2*xk;
				qk = qkm1 + qkm2*xk;
				pkm2 = pkm1;
				pkm1 = pk;
				qkm2 = qkm1;
				qkm1 = qk;

				if (qk != 0) r = pk/qk;
				if (r != 0)
				{
                    t = Math.Abs((ans - r) / r);
					ans = r;
				}
				else
					t = 1.0;

				if (t < thresh) return ans;

				k1 += 1.0;
				k2 += 1.0;
				k3 += 2.0;
				k4 += 2.0;
				k5 += 1.0;
				k6 -= 1.0;
				k7 += 2.0;
				k8 += 2.0;

                if ((Math.Abs(qk) + Math.Abs(pk)) > big)
				{
					pkm2 *= biginv;
					pkm1 *= biginv;
					qkm2 *= biginv;
					qkm1 *= biginv;
				}
                if ((Math.Abs(qk) < biginv) || (Math.Abs(pk) < biginv))
				{
					pkm2 *= big;
					pkm1 *= big;
					qkm2 *= big;
					qkm1 *= big;
				}
			} while (++n < 300);

			return ans;
		}

			
		/// <summary>
		/// Returns the continued fraction expansion #2 for incomplete beta integral.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <param name="x"></param>
		/// <returns></returns>
		private static double incbd(double a, double b, double x)
		{
			double xk, pk, pkm1, pkm2, qk, qkm1, qkm2;
			double k1, k2, k3, k4, k5, k6, k7, k8;
			double r, t, ans, z, thresh;
			int n;
			double big = 4.503599627370496e15;
			double biginv = 2.22044604925031308085e-16;

			k1 = a;
			k2 = b - 1.0;
			k3 = a;
			k4 = a + 1.0;
			k5 = 1.0;
			k6 = a + b;
			k7 = a + 1.0;
			;
			k8 = a + 2.0;

			pkm2 = 0.0;
			qkm2 = 1.0;
			pkm1 = 1.0;
			qkm1 = 1.0;
			z = x/(1.0 - x);
			ans = 1.0;
			r = 1.0;
			n = 0;
			thresh = 3.0*MACHEP;
			do
			{
				xk = -(z*k1*k2)/(k3*k4);
				pk = pkm1 + pkm2*xk;
				qk = qkm1 + qkm2*xk;
				pkm2 = pkm1;
				pkm1 = pk;
				qkm2 = qkm1;
				qkm1 = qk;

				xk = (z*k5*k6)/(k7*k8);
				pk = pkm1 + pkm2*xk;
				qk = qkm1 + qkm2*xk;
				pkm2 = pkm1;
				pkm1 = pk;
				qkm2 = qkm1;
				qkm1 = qk;

				if (qk != 0) r = pk/qk;
				if (r != 0)
				{
                    t = Math.Abs((ans - r) / r);
					ans = r;
				}
				else
					t = 1.0;

				if (t < thresh) return ans;

				k1 += 1.0;
				k2 -= 1.0;
				k3 += 2.0;
				k4 += 2.0;
				k5 += 1.0;
				k6 += 1.0;
				k7 += 2.0;
				k8 += 2.0;

                if ((Math.Abs(qk) + Math.Abs(pk)) > big)
				{
					pkm2 *= biginv;
					pkm1 *= biginv;
					qkm2 *= biginv;
					qkm1 *= biginv;
				}
                if ((Math.Abs(qk) < biginv) || (Math.Abs(pk) < biginv))
				{
					pkm2 *= big;
					pkm1 *= big;
					qkm2 *= big;
					qkm1 *= big;
				}
			} while (++n < 300);

			return ans;
		}

			
		/// <summary>
		/// Returns the power series for incomplete beta integral. Use when b*x is small and x not too close to 1.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <param name="x"></param>
		/// <returns></returns>
		private static double pseries(double a, double b, double x)
		{
			double s, t, u, v, n, t1, z, ai;

			ai = 1.0/a;
			u = (1.0 - b)*x;
			v = u/(a + 1.0);
			t1 = v;
			t = u;
			n = 2.0;
			s = 0.0;
			z = MACHEP*ai;
            while (Math.Abs(v) > z)
			{
				u = (n - b)*x/n;
				t *= u;
				v = t/(a + n);
				s += v;
				n += 1.0;
			}
			s += t1;
			s += ai;

            u = a * Math.Log(x);
            if ((a + b) < MAXGAM && Math.Abs(u) < MAXLOG)
			{
				t = Gamma(a + b)/(Gamma(a)*Gamma(b));
                s = s * t * Math.Pow(x, a);
			}
			else
			{
                t = LogGamma(a + b) - LogGamma(a) - LogGamma(b) + u + Math.Log(s);
				if (t < MINLOG) s = 0.0;
                else s = Math.Exp(t);
			}
			return s;
		}

        /// <summary>
        /// Return the value of the Airy Ai function at x.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double AiryA(double x)
        {
            double z, zz, t, f, g, uf, ug, k, zeta, theta, ai = 0;
            int domflg;

            domflg = 0;
            if (x > MAXAIRY) {
	            return ai;
            }

            if (x < -2.09) {
	            domflg = 15;
                t = Math.Sqrt(-x);
	            zeta = -2.0 * x * t / 3.0;
                t = Math.Sqrt(t);
	            k = SQPII / t;
	            z = 1.0 / zeta; 
	            zz = z * z;
	            uf = 1.0 + zz * polevl(zz, AFN, 8) / p1evl(zz, AFD, 9);
	            ug = z * polevl(zz, AGN, 10) / p1evl(zz, AGD, 10);
                theta = zeta + 0.25 * Math.PI;
                f = Math.Sin(theta);
                g = Math.Cos(theta);
	            return k * (f * uf - g * ug);
            }

            if (x >= 2.09) {		/* cbrt(9) */
	            domflg = 5;
                t = Math.Sqrt(x);
	            zeta = 2.0 * x * t / 3.0;
                g = Math.Exp(zeta);
                t = Math.Sqrt(t);
	            k = 2.0 * t * g;
	            z = 1.0 / zeta;
	            f = polevl(z, AN, 7) / polevl(z, AD, 7);
                ai = SQPII * f / k;
                if (x > 8.3203353)
	                return ai;
            }
            if ((domflg & 1) != 0)
                return ai;

            f = 1.0;
            g = x;
            t = 1.0;
            uf = 1.0;
            ug = x;
            k = 1.0;
            z = x * x * x;
            while (t > MACHEP) {
	            uf *= z;
	            k += 1.0;
	            uf /= k;
	            ug *= z;
	            k += 1.0;
	            ug /= k;
	            uf /= k;
	            f += uf;
	            k += 1.0;
	            ug /= k;
	            g += ug;
                t = Math.Abs(uf / f);
            }
            uf = AIRY_C1 * f;
            ug = AIRY_C2 * g;
            return uf - ug;
        }

        /// <summary>
        /// Return the value of the Airy Bi function at x.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double AiryB(double x)
        {
            double z, zz, t, f, g, uf, ug, k, zeta, theta;

            if (x > MAXAIRY)
                return Double.PositiveInfinity;

            if (x < -2.09)
            {
                t = Math.Sqrt(-x);
                zeta = -2.0 * x * t / 3.0;
                t = Math.Sqrt(t);
                k = SQPII / t;
                z = 1.0 / zeta;
                zz = z * z;
                uf = 1.0 + zz * polevl(zz, AFN, 8) / p1evl(zz, AFD, 9);
                ug = z * polevl(zz, AGN, 10) / p1evl(zz, AGD, 10);
                theta = zeta + 0.25 * Math.PI;
                f = Math.Sin(theta);
                g = Math.Cos(theta);
                return k * (g * uf + f * ug);
            }

            if (x > 8.3203353)
            {
                t = Math.Sqrt(x);
                zeta = 2.0 * x * t / 3.0;
                g = Math.Exp(zeta);
                t = Math.Sqrt(t);
                z = 1.0 / zeta;
                f = z * polevl(z, BN16, 4) / p1evl(z, BD16, 5);
                k = SQPII * g;
                return k * (1.0 + f) / t;
            }

            f = 1.0;
            g = x;
            t = 1.0;
            uf = 1.0;
            ug = x;
            k = 1.0;
            z = x * x * x;
            while (t > MACHEP)
            {
                uf *= z;
                k += 1.0;
                uf /= k;
                ug *= z;
                k += 1.0;
                ug /= k;
                uf /= k;
                f += uf;
                k += 1.0;
                ug /= k;
                g += ug;
                t = Math.Abs(uf / f);
            }
            uf = AIRY_C1 * f;
            ug = AIRY_C2 * g;
            return SQRT3 * (uf + ug);
        }

        /// <summary>
        /// Return the value of the digamma function (the logarithmic derivative of 
        /// the gamma function) at x.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double Digamma(double x)
        {
            if (x < 0.0)
                return Digamma(1 - x) + Math.PI / Math.Tan(Math.PI * (1 - x));
            else if (x < 1.0)
                return Digamma(1 + x) - 1.0 / x;
            else if (x == 1.0)
                return GAMMA;
            else if (x == 2.0)
                return 1 - GAMMA;
            else if (x == 3.0)
                return 1.5 - GAMMA;
            else if (x > 3.0)
                return 0.5 * (Digamma(x / 2.0) + Digamma((x + 1) / 2.0)) + LN2;
            else
            {
                double TN_1 = 1.0;
                double TN = x - 2.0;
                double TN1;

                double result = KNC[0] + KNC[1] * TN;
                x -= 2;

                for (int i = 2; i < KNC.Length; i++)
                {
                    TN1 = 2.0 * x * TN - TN_1;
                    result += KNC[i] * TN1;
                    TN_1 = TN;
                    TN = TN1;
                }

                return result;
            }
        }

        /// <summary>
        /// Approximately evaluate the sine integral at x. This approximation only converges
        /// for |x| <= 5π.
        /// 
        /// The sine integral is defined as
        /// 
        ///          x
        ///         /
        /// Si(x) = | sinc(t) dt
        ///         /
        ///          0
        ///          
        /// with sinc(t) = sin(t)/t.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double SiApprox(double x)
        {
            return Integrator.Simpsons(t => Math.Sin(t) / t, 1e-8, x, 1 << 10);
            // 24/12/2015: The following comment refers to a time when this code belonged
            // to an old version of Artemis. Currently, there is no class named UpperSITransition,
            // thus the commment is moot. However, the issue involving convergence still persists.

            // 7/6/2015: This approximation isn't convergent enough on x > 4π
            // to be used by UpperSITransition. The above statement works, but
            // is seriously inefficient. Consider implementing something from
            // the Cephes library later on.
            /*
            var x2 = x * x;
            // This is the Padé Approximant of Si, given by
            //      https://en.wikipedia.org/wiki/Trigonometric_integral
            return x*(1.0 
                + x2*(-4.54393409816329991e-2
                    + x2*(1.15457225751016682e-3 
                        + x2*(-1.41018536821330254e-5 
                            + x2*(9.43280809438713025e-8
                                + x2*(-3.53201978997168357e-10 
                                    + x2*(7.08240282274875911e-13 
                                        + x2*(-6.05338212010422477e-16))))))))
                / (1.0 
                    + x2*(1.01162145739225565e-2 
                        + x2*(4.99175116169755106e-5 
                            + x2*(1.55654986308745614e-7 
                                + x2*(3.28067571055789734e-10 
                                    + x2*(4.5049097575386581e-13 
                                        + x2*(3.21107051193712168e-16)))))));
             */
        }

        /// <summary>
        /// Approximately evaluate the cosine integral at x. This approximation only converges
        /// for |x| <= 5π.
        /// 
        /// The cosine integral is defined as
        /// 
        ///                      x
        ///                     /
        /// Ci(x) = γ + ln(x) + | (cos(t) - 1)/t dt
        ///                     /
        ///                      0
        ///                     
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double CiApprox(double x)
        {
            var x2 = x * x;
            // This is the Padé Approximant of Si, given by
            //      https://en.wikipedia.org/wiki/Trigonometric_integral
            return 0.577215664901532861 + Math.Log(x)
                + x2 * (-0.25
                    + x2 * (7.51851524438898291e-3
                        + x2 * (-1.27528342240267686e-4
                            + x2 * (1.05297363846239184e-6
                                + x2 * (-4.68889508144848019e-9
                                    + x2 * (1.06480802891189243e-11
                                        + x2 * (-9.93728488857585407e-15)))))))
                / (1.0
                    + x2 * (1.1592605689110735e-2
                        + x2 * (6.72126800814254432e-5
                            + x2 * (2.55533277086129636e-7
                                + x2 * (6.97071295760958946e-10
                                    + x2 * (1.38536352772778619e-12
                                        + x2 * (1.89106054713059759e-15
                                            + x2 * (1.39759616731376855e-18))))))));
        }

        // Convenience methods

        /// <summary>
        /// Return the value of sin(x)^2.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double SineSqrd(double x)
        {
            return Math.Pow(Math.Sin(x), 2.0);
        }

        /// <summary>
        /// Return the value of cos(x)^2.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double CosSqrd(double x)
        {
            return Math.Pow(Math.Cos(x), 2.0);
        }

        /// <summary>
        /// Return the value of the normal distribution at x (e^(-x^2)).
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double NormDistr(double x)
        {
            return Math.Exp(-Math.Pow(x, 2.0));
        }

        /// <summary>
        /// Return the value of the bivariate normal distribution at (x, y) (e^(-x^2 - y^2)).
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static double BivariateNormDistr(double x, double y)
        {
            return Math.Exp(-Math.Pow(x, 2.0) - Math.Pow(y, 2.0));
        }

        /// <summary>
        /// SoftMax is an infinitely differentiable approximation of the standard 
        /// Max function (i.e. it is smooth).
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static double SoftMax(double x, double y)
        {
            return Math.Log(Math.Exp(x) + Math.Exp(y));
        }

        /// <summary>
        /// SoftMin is an infinitely differentiable approximation of the standard
        /// Min function (i.e. it is smooth).
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static double SoftMin(double x, double y)
        {
            return -Math.Log(Math.Exp(-x) + Math.Exp(-y));
        }

        /// <summary>
        /// A scaled version of the SoftMax function with smoothing parameter <code>k</code>.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static double FuzzySoftMax(double x, double y, double k)
        {
            return SoftMax(k * x, k * y) / k;
        }

        /// <summary>
        /// A scaled version of the SoftMin function with smoothing parameter <code>k</code>.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static double FuzzySoftMin(double x, double y, double k)
        {
            return SoftMin(k * x, k * y) / k;
        }

	}
}
