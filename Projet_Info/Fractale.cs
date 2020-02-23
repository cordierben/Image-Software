using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Info
{
    class Fractale
    {
        byte[,][] image;

        public Fractale(byte[,][] image1)
        {
            image = image1;
        }

        public byte[,][] Image
        { get { return image; } }


        /// <summary>
        /// Méthode dessinant la fractale de Mandelbrot. Pour se faire, on calcule le module de la suite de Syracuse Zn+1=Zn^2+Z0 en fonction des 
        /// coordonées du pixel et on regarde s'il est inférieur à 2.S'il ne l'est pas, le pixel n'appartient pas à la fractale et on colore en fonction
        /// du nombre d'itérations nécessaire pour dépasser 4. S'il l'est, il appartient à la fractale et on laisse le pixel noir (choix esthetique)
        /// on le colore car il appartient à la fractale
        /// </summary>
        /// <param name="taille"></param>
        /// <returns></returns>

        public byte[,][] Mandelbrot(int taille)
        {
            int largeur = taille;
            int hauteur = taille;
            double x1 = -2.1;//Les coordonnées x1,x2,y1 et y2 correspondent au domaine sur lequel la fractale est visible
            double x2 = 0.6;
            double y1 = -1.2;
            double y2 = 1.2;
            double hauteur2=hauteur/(x2-x1);
            double largeur2 = largeur / (y2 - y1);
            byte[,][] fractale = new byte[hauteur, largeur][];//On rentre les dimensions de l'image
            for(int i=0;i<hauteur;i++)
            {
                for(int j=0;j<largeur;j++)
                {
                    fractale[i, j] = new byte[3];
                }
            }
            for(int i=0;i<fractale.GetLength(0);i++)
            {
                for(int j=0;j<fractale.GetLength(1);j++)
                {
                    double cr = (j-largeur2-largeur/3)/(largeur2+y1);//Ces deux formules permettent de centrer la fractale en fonction des dimensions rentrées
                    double cc = (i-hauteur2-hauteur/6)/(hauteur2+x1);//Mais seront aussi les valeurs qui permettront le calcul qui dessinera la fractale
                    double zr = 0;
                    double zc = 0;
                    double iteration = 0;
                    //Ici débute le dessin de la fractale. On va calculer Zn²+Zo un certain nombre de fois pour chacune des coordonnees, sachant que Zo=x+iy (i complexe)
                    //Comme Visual studio ne peut pas gérer les complexes, on développe et on isole partie réélle Zr+1=Zr*Zr-Zc*Zc+Cr et partie imaginaire Zc+1=2*Zc*Zr+Cc,
                    //avec Cr=x et Cc=y. On calcule le carré du module, soit Zr*Zr+Zc*ZC. Si cette valeur dépasse 4, alors elle n'appartient pas à la fractale, et si elle est
                    //inféieure à cette valeur après 150 itérations (on considère qu'elle ne dépassera pas 4 au delà de cette valeur) alors elle appartient à la fractale, et on 
                    //colorie le pixel.
                    do
                    {
                        double tmp = zr;
                        zr = zr * zr - zc * zc + cr;
                        zc = 2 * zc * tmp + cc;
                        iteration++;
                    } while (iteration < 150 && zr * zr + zc * zc < 4);
                    //Par la suite, on modifie la coloration en fonction du nombre d'itération pour dépasser 4 pour avoir un beau rendu dégradé.
                    for (int i2 = 0; i2 < 150; i2++)
                    {
                        if (i2 == iteration)
                        {
                            fractale[i, j][0] = Convert.ToByte(Convert.ToInt32(i2 * 1.5));//On va avoir une fractale avec une dominance de bleu autour, qui sera dégradé en 
                            fractale[i, j][1] = Convert.ToByte(Convert.ToInt32(i2 * 1.5));//fonction de la valeur de i2, et donc d'itération.Par choix, on laisse l'intérieur de la
                            fractale[i, j][2] = Convert.ToByte(Convert.ToInt32(i2 * 1.5));//fractale noire, qui sera donc entourée de bleu, pour un beau rendu!
                        }
                    }

                }
            }
            return fractale;
        }

        /// <summary>
        /// Méthode pour les fractales de Julia. C'est exactement le même fonctionnement que pour la fractale de Mandelbrot, mis à part qu'on peut 
        /// rentrer des valeurs de Zn dès le départ, pour modifier la forme de la fractale. De plus, on colore les pixels cette fois ci qui appartiennent
        /// à la fractale, en fonction de leur valeur au bout des 150 itérations.
        /// </summary>
        /// <param name="taille"></param>
        /// <returns></returns>

        public byte[,][] Julia(int taille)
        {
            int hauteur = taille;
            int largeur = taille;
            Console.Clear();
            Console.WriteLine("Vous allez maintenant saisir les valeurs de la fractale.");
            Console.WriteLine("Exemple de valeur avec de beau rendu :");
            Console.WriteLine("(0,285;0,01) ; (0,3;0,5) ; (-0,4;0,6) ; (-0,038088;0, 9457);(-0,75;0,1)");
            Console.WriteLine();
            Console.WriteLine("Veuillez saisir la partie réelle de votre fractale (entre -1 et 1)>");
            double re= Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Veuillez saisir la partie imaginaire de votre fractale (entre -1 et 1)>");
            double im = Convert.ToDouble(Console.ReadLine());
            double x1 = -1.5;
            double x2 = 1.5;
            double y1 = -2;
            double y2 = 2;
            int hauteur2 = Convert.ToInt32(hauteur / (x2 - x1));
            int largeur2 = Convert.ToInt32(largeur / (y2 - y1));
            byte[,][] fractale = new byte[hauteur, largeur][];
            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur; j++)
                {
                    fractale[i, j] = new byte[3];
                }
            }
            for (int i = 0; i < fractale.GetLength(0); i++)
            {
                for (int j = 0; j < fractale.GetLength(1); j++)
                {
                    double zr = (j - largeur2 - largeur / 3.75) / (largeur2 + y1);
                    double zc = (i - hauteur2 - hauteur / 6) / (hauteur2 + x1);
                    double cr = re;
                    double cc = im;
                    double iteration = 0;
                    double a = 0;
                    double b = 0;
                    do
                    {
                        double tmp = zr;
                        zr = zr * zr - zc * zc + cr;
                        a = zr;
                        zc = 2 * zc * tmp + cc;
                        b = zc;
                        iteration++;
                    } while (iteration < 150 && zr * zr + zc * zc < 4);
                    for (int i2 = 0; i2 < 150; i2++)
                    {
                        if (i2==iteration)
                        {
                            fractale[i, j][0] = Convert.ToByte(Convert.ToInt32(i2*1.5));
                            fractale[i, j][1] = Convert.ToByte(Convert.ToInt32(i2*1.25));
                            fractale[i, j][2] = Convert.ToByte(Convert.ToInt32(i2*0));
                        }
                    }
                    double c = a * a + b * b;
                    //Longue modification des couleurs, pour un beau rendu!
                    if(iteration==150)
                    {
                        if ( c > 0 )
                        {
                            fractale[i, j][0] = Convert.ToByte(41);
                            fractale[i, j][1] = Convert.ToByte(176);
                            fractale[i, j][2] = Convert.ToByte(243);
                        }
                        
                        if(c>0.03)
                        {
                            fractale[i, j][0] = Convert.ToByte(40);
                            fractale[i, j][1] = Convert.ToByte(220);
                            fractale[i, j][2] = Convert.ToByte(244);
                        }
                        if (c > 0.06)
                        {
                            fractale[i, j][0] = Convert.ToByte(29);
                            fractale[i, j][1] = Convert.ToByte(255);
                            fractale[i, j][2] = Convert.ToByte(255);
                        }
                        if (c > 0.09)
                        {
                            fractale[i, j][0] = Convert.ToByte(255);
                            fractale[i, j][1] = Convert.ToByte(255);
                            fractale[i, j][2] = Convert.ToByte(255);
                        }
                        if (c > 0.12)
                        {
                            fractale[i, j][0] = Convert.ToByte(233);
                            fractale[i, j][1] = Convert.ToByte(255);
                            fractale[i, j][2] = Convert.ToByte(29);
                        }
                        if (c > 0.15)
                        {
                            fractale[i, j][0] = Convert.ToByte(234);
                            fractale[i, j][1] = Convert.ToByte(190);
                            fractale[i, j][2] = Convert.ToByte(50);
                        }
                        if (c > 0.2)
                        {
                            fractale[i, j][0] = Convert.ToByte(241);
                            fractale[i, j][1] = Convert.ToByte(43);
                            fractale[i, j][2] = Convert.ToByte(57);
                        }
                        if (c > 0.25)
                        {
                            fractale[i, j][0] = Convert.ToByte(166);
                            fractale[i, j][1] = Convert.ToByte(14);
                            fractale[i, j][2] = Convert.ToByte(43);
                        }
                        if (c > 0.3)
                        {
                            fractale[i, j][0] = Convert.ToByte(241);
                            fractale[i, j][1] = Convert.ToByte(43);
                            fractale[i, j][2] = Convert.ToByte(57);
                        }
                        if (c > 0.35)
                        {
                            fractale[i, j][0] = Convert.ToByte(234);
                            fractale[i, j][1] = Convert.ToByte(190);
                            fractale[i, j][2] = Convert.ToByte(50);
                        }
                        if (c > 0.4)
                        {
                            fractale[i, j][0] = Convert.ToByte(233);
                            fractale[i, j][1] = Convert.ToByte(255);
                            fractale[i, j][2] = Convert.ToByte(29);
                        }
                        if (c > 0.45)
                        {
                            fractale[i, j][0] = Convert.ToByte(255);
                            fractale[i, j][1] = Convert.ToByte(255);
                            fractale[i, j][2] = Convert.ToByte(255);
                        }
                        if (c > 0.5)
                        {
                            fractale[i, j][0] = Convert.ToByte(29);
                            fractale[i, j][1] = Convert.ToByte(255);
                            fractale[i, j][2] = Convert.ToByte(255);
                        }
                        if(c>0.6)
                        {
                            fractale[i, j][0] = Convert.ToByte(40);
                            fractale[i, j][1] = Convert.ToByte(220);
                            fractale[i, j][2] = Convert.ToByte(244);
                        }
                        if (c > 0.8)
                        {
                            fractale[i, j][0] = Convert.ToByte(41);
                            fractale[i, j][1] = Convert.ToByte(176);
                            fractale[i, j][2] = Convert.ToByte(243);
                        }
                        if(c>1)
                        {
                            fractale[i, j][0] = Convert.ToByte(35);
                            fractale[i, j][1] = Convert.ToByte(135);
                            fractale[i, j][2] = Convert.ToByte(225);
                        }
                        if (c > 1.3)
                        {
                            fractale[i, j][0] = Convert.ToByte(41);
                            fractale[i, j][1] = Convert.ToByte(176);
                            fractale[i, j][2] = Convert.ToByte(243);
                        }
                        if (c > 1.6)
                        {
                            fractale[i, j][0] = Convert.ToByte(40);
                            fractale[i, j][1] = Convert.ToByte(220);
                            fractale[i, j][2] = Convert.ToByte(244);
                        }
                        if (c > 2)
                        {
                            fractale[i, j][0] = Convert.ToByte(29);
                            fractale[i, j][1] = Convert.ToByte(255);
                            fractale[i, j][2] = Convert.ToByte(255);
                        }
                        if (c > 2.2)
                        {
                            fractale[i, j][0] = Convert.ToByte(255);
                            fractale[i, j][1] = Convert.ToByte(255);
                            fractale[i, j][2] = Convert.ToByte(255);
                        }
                        if (c > 2.4)
                        {
                            fractale[i, j][0] = Convert.ToByte(233);
                            fractale[i, j][1] = Convert.ToByte(255);
                            fractale[i, j][2] = Convert.ToByte(29);
                        }
                        if (c > 2.8)
                        {
                            fractale[i, j][0] = Convert.ToByte(234);
                            fractale[i, j][1] = Convert.ToByte(190);
                            fractale[i, j][2] = Convert.ToByte(50);
                        }
                        if (c > 3.2)
                        {
                            fractale[i, j][0] = Convert.ToByte(233);
                            fractale[i, j][1] = Convert.ToByte(255);
                            fractale[i, j][2] = Convert.ToByte(29);
                        }
                        if (c > 3.6)
                        {
                            fractale[i, j][0] = Convert.ToByte(255);
                            fractale[i, j][1] = Convert.ToByte(255);
                            fractale[i, j][2] = Convert.ToByte(255);
                        }
                    }

                }
            }
            return fractale;
        
        }
    }
}
