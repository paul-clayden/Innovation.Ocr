using System.Drawing;
using IaG.State.Innovation.Ocr.DtkAnpr;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.ComponentModel;
using System.Text;

namespace Innovation.OcrQuoteBuy.Controllers
{
    public class OcrController : ApiController
    {
        /// <summary>
        /// POST api/ocr - Post an image of vehicle plate as header 'Image' (Base64 string). Match returns plate number as string
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public string Post()
        {
            string img = null;
            IEnumerable<string> image;
            //try
            //{
                var content = Request.Content.ReadAsStringAsync().Result;
                //if (!Request.Content.Headers.TryGetValues("Image", out image))
                //    throw new HttpResponseException(HttpStatusCode.InternalServerError);

                //img = image.First();
                var byteArray = Convert.FromBase64String(Encoding.ASCII.GetString(HttpUtility.UrlDecodeToBytes(content)).Replace("Image=", "").Replace("\r", "").Replace("\n", "").Trim());
                //var byteArray = "/9j/4AAQSkZJRgABAQAAAQABAAD/2wCEAAkGBxQSEhQUEhQUFRUUFBQUFBQVFBUUFBQUFBQWFhQUFBQYHSggGBolHBQUITEhJSksLi4uFx8zODMsNygtLisBCgoKDg0OFBAPGCwcHBwsLCwsLCwsLCwsLCwsLCwsLCwsLCwsLCwsLCwsLCwsLCwsLCwsLCwsLCw3LCwsLCwsLP/AABEIAMIBAwMBIgACEQEDEQH/xAAcAAABBAMBAAAAAAAAAAAAAAABAAIEBwMFBgj/xABVEAABAwICBQYHCQwIBAcAAAABAAIDBBEFEgYHITFBE1FhcYGRIjJyobHB0RQjQlJTYpKTwhUWM0NEVFWCssPS0yRjg4Si4eLwNEV0owglZHWUs/H/xAAXAQEBAQEAAAAAAAAAAAAAAAAAAQID/8QAGxEBAQEAAwEBAAAAAAAAAAAAABEBAhIhQTH/2gAMAwEAAhEDEQA/AOfTggE5QEIgJAIhAQiEkbICigiFAkUkUARSSsgSSKVkASRskgCCKSKakUbJIG3QRslZQNQKNkrIG3QunWQKBt0CUULKKBcmlycU2yAZkLo2QIQNzJJJKKzgIgIBOC6MDZEIItKgcEUgigSKCcAgSKVkbIAilZGyAIJ1krIGpI2SQBJFJRTUE5JA1BOQsgahZOsggagnFNUAKaU6yCBpTU8hNKKaU0pxQKgagikipATgsbSsgXRg4JWSRUCATgEE4IFZKyKKAIpJWQFFBOsooIEJ1kkDUkSErIBtSTkLIAgUUlA1BOTX3sbb7bOvggCBUHDnzZXGYC+bwQLbrcbdKkEuQZUCsd3LIEAQsnFNKigQmkJxTSgaU0pxCBQMSSSRWRm9ZQo7XbVmzLbDInALGHpweED7JwCYHhODwgdZFNDgnBwRSsjZK6IUCRSskgSSKVkASRslZQNSTkLIpqCdZAhA1Io2QIQNLbp9kELqBWTXBG6BQMKBTigUDCmlPQKBiaU8pjyisZcggEkGeSE8E2aUMYXvuANgA3ucdzQpl0zEo8zaPprWg9Pi71tmJVDoticzA9lNG1rhdoefCsdxPhKSzQbFj+Jp/p2+0ranweR1RDM2dzWRtAMQvZ2w9NrG/EcFu1RRv3jYt8jT/Wf6kfvIxb5Cm+s/1K8UkiKLdoZiw/Jqc9Un+tMOiOLD8kg+sH8xXumSsu0gEgkEXG8XG8JFUYNEsX/M4frR/MTjohi/5pB9cP5it/RvDJKaLk5Z3TuzEh7hYhptZu8nnPatqkFFfepi/wCZRfXN/mJh0axYfkDD1TN/mq+EEgob73cW/R4+ub/MR+9/Ff0d/wB9n8xXygkKoc4Hin6Nd9ez+JMOD4mN+GydkzPar8QSFUEcMxH9Gz9krfYmmgxD9G1P0x/Ar/SSFUB7gr/0bVfSafsJppK0b8OrOwX/AHa9ApKQrz05lUP+X131Tj9hYHS1A30NcP7u8/ZXoxJIV5wNXLxo64f3Z6Ya9/GmrB/dne1ekkkhXml2K23xVI66cj7SZ92m8WzDrhP8S9DaQNqDCfchYJczSM/ilocMwuQbG1+Cnx3sM1r2F7br8bKQeZzj0Y3l464z7Uw6RQ/Ht1tcvTZYOYJjqdp+C3uCQrzN98UHyje5/sU+hro5r8m8OtvtcHuO1eg5qCIg+9Rk2Nrsbv6diqnWDhOSigrJKeOnqY6hscnJZQHwvcWWdlJBBuCL7R0XIUiubcxR5VJmNgVHYy+0rLQCNJZkkRHxGsEbbnsA3lOp6kSwUZta9eONzsyjetDWtMrySdnAdC3WEx2pqMc2IesLtjOPSce4dQTk1m4dSciKp1z6VT0z4IqeV0ZLXPeW2udtmi/eq8w/WFXskY91VI5rXtc5psQ5oIzNItxFwpmuOu5XEpADcRtZH3C585XENWcV7BikDmhw3OAI6iLhPXOau6/l8NpX3uRE1jj86PwD+yujWkURrU0orIMRkjiqJY2NbGWta6wF2Anz3XIHTfEPzyf6a3mulv8A5nJ0xxH/AArt9R2GQPopHvijc/l3NLnMa42DWlouRu2rOKqwabV/55P9YpeGab14miPuuZ3vjPBLswcMwu0g777l6O+5EHyEP1bPYizCoAQRDECNoIjYCOo2VEsLjdY2nLcNjDWAPqJASxh3Nb8d/RzDiuzXmTWhWOkxOqLvgP5No5msaAAPOe1NRJ+/vFqhxMc0xttLYYgQ0dIa02HWtxozrbqoJA2s9+jvZ5yhkzOciwAdbmI7VZ+rDDGQYbT5ALyxtle7i58gzEk8bXA6gq/1+YMxj4KlgAdJmjksLZi0ZmuPTbML9ARVx0FayeNksTg9j2hzXDcQVjxbEY6aF80zsscbS5x9AA4kmwA5yq11BYm58FRA4kiJ7Xs6BKDmb3tJ7Ssuv2sc2kgjBs2Sa7ukRsJA7yD2IOM0j1t1szz7ncKaK/ghoa6Qjne9wIv0DzrRjWNiX55J3R/wrt9R2jEEzJamZjZHMk5ONrwHNZZocXZTszbd/Qre+5sPyUX1bfYg4PUxpLUVsVQKmQymN7MriGg2e03acoF93nW21rYtPSYe6WnfybxJEC4AEhrnWNswI3kLrIKdjBZjWtB2kNaG367Lktb0GfCan5vJP+jMwofXG6q9Ys89V7nrJc4lHvTi1rSJBty+CBcEX7QrjXjqmqHRva9hLXMcHNI3hzTcFeqtDcfbXUkU7bXc20gHwZG7Hjv29RCgqXTbWDX0uI1EUcwEccjcrDGwjKWMda5F/hc6u2GoBjbIbAFgeTwALbrzrrngy4rN89kT/wDBl+wrme98uC3juXvoDlA3lxgtYdKfRWOlGuOpfK4UWWOFpIa9zQ98gHwzfY0HgLXt3LTx628TBHvsZ6DEzb3BYNW2j3L4hCyoheYvCcQ+NwYS1pIDri1r22K/ptFKJzS11JTEHZbkY/SBsQczq51itxG8MrRHUNF7DxJGjeWX3EcQputumz4TVfMDJPoSNd6iqVni+5uNFsJIbBVtDdt/e3lpyk8fBfZegdM6blaCrZbxqeUD6Bsmfq/VFsfmaDa17W6bhSANiiYX4UUbvmN8wspZXNQSQSRXPNkW8wrbS0hv/wAw3doXPsaAt/g3/CUn/uHbvC7658XpBm4dSLnWBPNtQZuHUFrNKqzkaOok3ZYnkdZFh6VnfweZNI6kz1c7xtMkzrdN3Wb6l0msjQ1mHinMRcRIyzy43vIACSOYG52dC0eiFHy1dTM35p2E9TTmd5mlXJrtw/lKEScYpGnsPgn0qb5mKiahq/NSTQk/gpswHzZGg/tB6s9UPqMr8ldJEd00J+lG4EeZz1fC0jz3rvbbEieeGP7QTtXOsRmHQPhkhfJmkMgcxzRvaAQQerzrJr0b/T2nngZ+05QdXegH3SZLI6bkhG/JYMzEnKHXO0WG0LOK7Y67IeFLL9NisHRfHWV1Myoja5ofm8F1rtLXFpGzftCroakmfnb/AKofxKwdEsAbQUzKdry8NLjmIAJL3Fx2DdvWhuVRGu7Rl0VR7saLxz2D/myhttvlAd4KvdUZrv0odJN7iZ+Diyuk53SEXA6gCO09CmmJ2rbWbT09Kymqy5hiu2OQNL2uZvANtoI3c2wLnNa+m8eIviZAHclDmOZwyl73WFw3eAAOPOVn1easzXxGomldFESWsDAC9+U2Lru2AXuN22yi6xNXLsOa2aOQywOdlJcAHxuO7NbYQecWQWFqNwR0NG+d4salwc0f1bBZp7SXHqIUfX/Fejp3fFqLd8T/AGBQtSGlzn3oZTfIzNTu45G+NGeq4I7eZbnXrFfDQfizxnvDh60FNaOaZ1dCx7KaQNa92Ygta7wgLXF92wDuW2GtTE/lx9VH7FH1Y6Nx19aIpieTax0jmg2L8pADbjaB4XDmV0SarcMII9z2uLXEstx0g5t6g5jQHWyZ5WwVwY1zzlZM0ZW5jubI0nZfdcLudYcOfDK0f+nkP0W5vUvN+luEijrJ6dri4RPs1x2EtLQ5t7cbOHcvRdJMarCA47TLRG55yYiD51cHl2KFz3ZWNc5x3Na0uds2mwG1WRqP0n5CpNM93vdR4tzsbMB4NvKGzsC0uqGbLitN87O3vjPsWTWho+cPxAui8GOU+6ICPguzXe0eS7b1OaoNvr8p7YhE/wCPTNHayR9/2grX1ZT58LpDzRZfokt9SpHWRpVFiLKGRpPLMhkbO3KRlcTHax3EEhx2K29S8+bCovmvlb/jJ9aDuLLTaXaSxYfTumlPRGweNJIR4LGj18ACeCnYxikVLC+aZ2WOMXcfQAOJOwAcV5k020rlxKoMj7hgu2GK98jCfO87Lns4BBiw90uIYkxztslRUte624XeCbdDWjuC9S1UWZj2nc5rh3iyrnVHoH7kYKqoHv8AI3wGH8Ux3P8APPm3Ky7KDzDhVc2KFrHZi5mZps0nxXEb9yyvxocI5D9AfaWLFIMlRUs+JVTjszkj0qG5pV64bqUccPyZ+mPYkoGRJOuF1kZD0rocGaPclKdmyvG3jvWhzBb/AAj/AIOl/wCu+0umpxejGbh1BcRrjreTw57eMr2M7L3PoXbx7h1D0Kp9e1ZspoR8+QjuaPWsaKlwyukp5WTRGz43Zmm1xe1to5rEro8c1h1tXC6GV0eR9swbGAdnTdb7VHojDWGaSoZnZGWsY25AzG5cTbfsy96s37wMOtb3Kzvd7VdVQehNfyFfSyXsBMxrvJkOR3mcvUS8r6RYf7nqpohs5ORwb1Xu3zWXpbRvEBUUsEw/GRMcestFx33TNuCm9e7P6bEeeAeZ7lv9QDveaof1rD3sHsWn18t/pMB/qT+0lqY0ipqVtS2ombEXuY5uc2BABBsedTBd6S5l2sDDh+Vxd5WJ+sfDR+UtPUHH0BVHVryxp28nEKsnfy8nmdYeYBeoaOqZKxskbg5j2hzHDc5rhcELz5rjwUwV75LeBUASNPzrAPHeL9qmquXVw1owyky7uRYe0jwvPdDWRA1+GVYduELnDymeE3zgKttVusaGlg9y1Zc1rXExSgFwAcbljgNosSbHp6FI1nayaeemdTUhc/lLCSSxa0MBBLRfaSbAc1rqjg9XFUY8TpHDjLlPU9rmn0q6ddEd8Km+a+E/91o9aqHVPhhnxOCw8GImV54ANBt/iIV7af4W6pw+piZtcY8zRzuYQ8DtyqCldSUuXFGj40Uo/ZPqXoteTNGsafQ1UdQwXMZ2tOwOaRZzTzbFbJ14w2/4Wa/lMtfvTBwmuKLLis/zhE7vjaPUrn1aO5TCKYH5FzD2FzV580u0gdX1UlQ9oZmsGsBvla0WAvxPG/SvQuqykdFhdM14sS1z7HeA9xcPMUFBaCv5LE6T5tQ1p7Tl9avbWtoz7uoX5BeaC80XOS0eGz9Zt+0BUORyOJnhydae5s69UhB4zIXoHUHNegkb8Sd3+JrSq31uaMe4q1zmC0NReWPma4n3xnYTfqcOZdt/4ep/eqtnNJG7vaR6lEanX3j7nzx0bSckTRLIODpH3yA+S3b+us+pzQIPy11S27Rtp4zxIP4V3RzDtXG62SfutV3+NHbq5GOy2WH63a2GGOJjKfLGxrGkxuvZosL2cBwTVeiigvPTtcuIn83H9k71vVrasNJZcQozLPl5RsjmHIMoIFiNl9h2oKj01hyYnXN/rmPH68TXHzrTuau01jUwbjEuz8JTxP287SWH9kLT8n1KbymrK54sPMe4pLoMiSnc6tDYLoMEH9Dpv+t3frLnwulwFv8AQ4Oirv5121ji9CR7h1BUbramMte4cI2NYOu2Y+lXjG7Y3pA9CwzYfE85nxxuPO5jSe8hY1XHanqHk6HMRtlle7sHgj9ld0mxxhoAaAANwAsB1BOVFD63MHc2uMjWktla11wCRcbD6FYOp+V5w5rXhw5OSRjbgi7b5ha/DwiOxdsWgpAKZ4qo9eGFyyyU7443vAY9pLGl1jcGxsFWTNG6s7qaoP8AYyexem6PGIZZZYY5A6SAtErBe7C4XF1PSDy03ROtO6kqPqX+xZW6F15/JKj6tw9K9QJJBodBKOSHD6WOVpa9kTQ5p3t6D0rJpXo1DiEJimBFjdjxbMx3OPWOK3SSqKDxTU5WsceRdFM3gc3Jut0tds86x4dqdrnuHKmGJvEl+c9jWjb3hegElItc3oXodBhsZbFdz325SV1szrbgB8FvQukSSVRWWmeqSKqkdNTSCB7yS9hbeJzjvcLbWE9o6FyzdSNVxqKcfWH7KvZJSLVT6M6mY4pGyVcwmym4jY0tYSN2YnaR0bFazWgCw2AbAOYIkpjpWje4DtCIrDF9UPLVclQKnIJJuVyclci5zEZs3PfgrRaLADoUaTEYW+NLGOt7R61Hk0gpW+NU0465ox60VC0z0VixKAQyktLXB7JG2zMI2G1+BBIUHQbQWPDDKY5ZJOVDQc4aLZb2tYdK2Uul9A3fWUw/to/atBW6W0HuqKcYlEGsY5joQ8ua65uHbDa/WDuCHp+k+rOkrqh1RK6Zr3NaHBjmhpyiwO1pN7WHYtaNTGH89Qf7Uepq3kmsrCx+WR9gcfQFFk1rYUPym/VFKfsqHqENTmG/FmPXM71LpdHdHqfDonMguxhdndneXbbAXu7qXPSa38MG6SV3VDJ6wFqcd1o4fUwvhdFWOa8WJZEAdhBBFzzgbwoqDrbZlxGlf8pTyN+g4H7S5arDi0hhs62w9KfpNpCa2aneyCWKCljdHHyotJI5waD0bA0LXTVEh3WaPOpyXE2O9hm2mwueniktSXSc57wkpBhiXX6GxmeldHGLywy8rk4ub0c6immb8Udyy0b3xPD4iWOG5w/3tW95VnMixfutSTS09TJJKyWnDm8mGktJIs7MAFvTphTfGf8AQcqwk0gqHEuLIC473GIXKIx6q4ck3qiZ6wrUWS7TamHyh/U/zUd+n1MNzJj1MH8S4H7uVfyoHVHGPsrGMQqPlXdlh6AlHdHWHH8GmqndTG/xJh0/cfFoKt36oC4puIVA3TSDqcR6EDWTHfNJ9N3tSjqItKZmSSSRYRKHy5eUfcNc/LsbmOTba5WZ2mmIcMLd+tOB9lcY6R53vcf1isZYeJKlV2R0xxM7sPhb5VU3/JY3aV4qd1PQt8qoB+2uR5PpS5PpSjpn6TYsfhYYzrkJ+2sUmkWKcavDW9TZHe1c6WDnTHFo4pR0DsfxDjiVG3yKZ7vsKPJjNad+MW6GUPrLFoX1cY4hYDiMalVvjiNQfGxipPk0rG+khYXVUh34piJ8lrGfvFqDXx//AIEw4gODHFOw2jsp8avxV39s1v2ysT4IDvnxJ/lVlvQ0rXGqedzAOsphfIeLR2JRsnYfREbRVOPzquQ/u004ThnwoJD1zSu/hWtLHHe89mxM9zt43PWVPVqe+jwtu6mb+sZT++WCSSiHiUlOeuOY/vlgEbR8EJ17brJNTscKyH4NFS//AByf2nlP92geLS0rf7vB62lYC5C6QqR90njcyFvVDT/ykRjU43Fo6mxt9Ea181QxvjOaOsgKDLjULfxgPkgu9ASFb12OVPyluouHossMmLVJ/HPHVJKPtrnZdJI/gte7uCiSaRPPixgdJJPsSFdJNM55u9znnnc4uPeSSsRXJzYzOeIb5LR6TdQpayR3jPcf1j6FqI7Yu6QguDypIi4gVExipkjjLom5ncyc6sYOIWJ+LMHFRUuglc6NrnjK4gEjmKlBaR2M8wKxnFXnc1KroAUs4XOmomd0IcnId71B0Lqho4rC/EGD4QWkFJzuJT20jekojYvxdg6VidjPM0lYGxNHALINis0pHEZDuamGWY8QE/MjmSFYjC8+M89iXuQcST2rI5yWZIlM9yt5vOninaOAWGauYzxnsb1uChy6QQN/GA+SCfQr1LraWA3AdyBcudn0tjHise7pNmj2qC/SmV3iRtH0nexIOuugSuHqcbqTsLsvQGgelQpKmR/jSPPW4+hWDv5qtjfGe1vW4BQZcegb+MB8kF3oXGMgClRU7eZIlb2XSiP4LHu7goz9IZXeJEB1kn2LDCwDgO5SLbEhUCTGKh2zOG9QA9N1EqJ5HWLpHm4+MbdyzyRgl/RZw9aDnNyAWF/OhUXkAjyY5lkDyQBbcLdnBAg9SBrdiysgDnAk2v6VhITHOtxQZuSu7INpvZu4b+BJUaSNJjSTsTpYyDtI71BHSWbkerzpK1XbtpRxJWZtO0cEgUXygDeB2qTFuntaBuATwVCdWMG9wWCTGWDdmPUERtbo5lozjZ+DG7t2KPLjEvBrQqOkDkcy5N1XO/4YA6L+pYTA8+NIUmjrZKtjd7mjtCiy4zEPhX6h6yucbRDiSe1ZH0jcpsOBSDZy6SxjcCe1RZdK/is71z/IBERjmUE+bSSd24hvUNvnUOWumfve89pHoQARVEfkinCHpWZAoMbYfG+aAevbtWyw5223OB5rqCw+F5TXD1p0NRlsegj0JiMmJnw+wKM1Pc8vNyi2IIC14T+UJ3d6aQG70m1Q27Cbbdiozx5+fvWZlM52xzyoJxHmb3pj8ReeIHUEE2qYIgLbS7YTdRGm9rbSL7tptz7FFknc7xiSs9PUmN/KR8ARt5ntLSD2EqB4qQDsRM1zaygsCzRnaOtBMqW8mbHfs86jtYXbSs1bUcq+/CwHd/vzLDI+/V6UCeR8Hv8A8kmwE7fOTZOggc4+CFOOFkAlxuRtPUpViByQ+M3vKSkmBvR3pKzUuNrVyu5z3lRGm+/akko2lRMHMEXhFJXizoBRJt6SS0ydBuWVJJUILINx6kUlnVxo0EEllRQKCSIKBSSQJvjN6/UsbvFCSSuDPHuT2bj1esJJKKbVeKexRIvheSfUgkrgxpJJIEtiPwcnkM/+xqSSCC3esttiSSDJH4p/3wWMIpKDf4L+DPlepS5tx8k+hJJY39axqC0cwQSSW6y//9k=";
                var stream = new MemoryStream(byteArray);
                stream.Position = 0;
                return ProcessImage(new Bitmap(stream));
                //return content;
            //}
            //catch (Exception ex)
            //{
            //    throw new HttpResponseException(HttpStatusCode.InternalServerError);
            //}
        }

        private string ProcessImage(Bitmap img)
        {
            IOcrProcessor processor = new DtkOcrProcessor();

            try
            {
                return processor.ReadText(img);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        //[System.Web.Http.HttpPost]
        //public string Post()
        //{
        //    try
        //    {
        //        var files = HttpContext.Current.Request.Files;
        //        string[] extensions = { ".jpg", ".jpeg", ".gif", ".bmp", ".png" };
        //        //if (!extensions.Any(x => x.Equals(Path.GetExtension(files[0].FileName.ToLower()), StringComparison.OrdinalIgnoreCase)))
        //        //{
        //        //    throw new HttpResponseException(HttpStatusCode.BadRequest);
        //        //}
        //        IOcrProcessor processor = new DtkOcrProcessor();
        //        var image = new Bitmap(files[0].InputStream);

        //        try
        //        {
        //            var result = processor.ReadText(image);

        //            if (!string.IsNullOrEmpty(result))
        //                return result;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new HttpResponseException(HttpStatusCode.InternalServerError);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        HttpContext.Current.Response.Write(ex.ToString());
        //    }
        //    throw new HttpResponseException(HttpStatusCode.InternalServerError);
        //}

        //Attempt to make this post asynchronous to solve why it fails on Azure but not local
        //[System.Web.Http.HttpPost]
        //public async Task<string> Post()//int length)
        //{
        //    var length = 55274;
        //    var inputStream = HttpContext.Current.Request.InputStream;
        //    byte[] buffer = new byte[length];
        //    await inputStream.ReadAsync(buffer, 0, length);
        //    string[] extensions = { ".jpg", ".jpeg", ".gif", ".bmp", ".png" };
        //    if (!extensions.Any(x => x.Equals(Path.GetExtension(HttpContext.Current.Request.Files[0].FileName.ToLower()), StringComparison.OrdinalIgnoreCase)))
        //    {
        //        throw new HttpResponseException(HttpStatusCode.BadRequest);
        //    }
        //    IOcrProcessor processor = new DtkOcrProcessor();
        //    var memStream = new MemoryStream(buffer);
        //    memStream.Position = 0;
        //    var image = new Bitmap(memStream);
        //    try
        //    {
        //        return processor.ReadText(image);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new HttpResponseException(HttpStatusCode.InternalServerError);
        //    }
        //}

    }
}
