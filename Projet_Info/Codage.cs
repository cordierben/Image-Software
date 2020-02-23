using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Info
{
    class Codage
    {
        byte[,][] image;

        public Codage(byte[,][] newimage)
        {
            image = newimage;
        }

        public byte[,][] Image
        {
            get { return image; }
        }


        /// <summary>
        /// METHODE DE CODAGE D'UNE IMAGE : On prend les couleurs de chaque pixel, on les convertit en binaire, et on en prend les 4 premiers 0 et 1.
        /// Puis on prend une seconde image, on convertit en binaire les couleurs et on prend les 4 premiers 0 et 1.
        /// Puis on crée une nouvelle séquence de 8 0 ou 1, avec les 4 premiers venant de l'image qui cache, et les 4 derniers de l'image à cacher.
        /// On convertit cette séquence en octet et on obtient une nouvelle matrice de pixel.
        /// </summary>
        /// <param name="image2"></param>
        /// <returns></returns>


        public byte[,][] codage(byte[,][] image2)
        {
            int a = 0;//Dans la première partie de la méthode, on détermine laquelle des deux images est la plus grande, pour ajouter des bords noirs
            int b = 0;//sur la plus petite image pour pouvoir les coder en intégralité l'une dans l'autre.
            if(image.GetLength(0)>image2.GetLength(0))//si l'image 1 a plus de lignes...
            {
                a = image.GetLength(0);//...alors le nombre de ligne sera celui de l'image 1
            }
            else
            {
                a = image2.GetLength(0);// sinon c'est celui de l'image 2
            }
            if(image.GetLength(1)>image2.GetLength(1))//Maintenant idem avec les colonnes
            {
                b = image.GetLength(1);
            }
            else
            {
                b = image2.GetLength(1);
            }
            byte[,][] imagebis = new byte[a, b][];//a et b ont donc les dimensions les plus grandes. On créé donc deux nouvelles images identiques
            byte[,][] image2bis = new byte[a, b][];//mais avec des bords noirs pour la plus petite
            for(int i=0;i<a;i++)
            {
                for(int j=0;j<b;j++)
                {
                    imagebis[i, j] = new byte[3];//On attribue à chacune des copies 3 index pour chaque pixel (rouge, vert et bleu)
                    image2bis[i, j] = new byte[3];
                }
            }
            for(int i=0;i<image.GetLength(0);i++)
            {
                for(int j=0;j<image.GetLength(1);j++)
                {
                    imagebis[i, j] = image[i, j];//On recopie donc la première image sur sa copie...
                }
            }
            for (int i = 0; i < image2.GetLength(0); i++)
            {
                for (int j = 0; j < image2.GetLength(1); j++)
                {
                    image2bis[i, j] = image2[i, j];//...puis on fait de même sur la seconde
                }
            }
            byte[,][] image3 = new byte[a, b][];//Et on créé enfin l'image qui sera le codage des deux autres avec les dimensions a et b
            for(int i=0;i<a;i++)
            {
                for(int j=0;j<b;j++)
                {
                    image3[i, j] = new byte[3];//On attribue 3 index à chaque pixel de la nouvelle image (rouge, vert et bleu)
                    for(int k=0;k<3;k++)
                    {
                        byte[] bin1 = byte_to_bin(imagebis[i, j][k]);//On transforme chaque valeur entière RVB de chaque pixel en binaire dans un tableau 
                        byte[] bin2 = byte_to_bin(image2bis[i, j][k]);// de 1 et 0 pour les deux images
                        byte[] bin3 = { bin1[0], bin1[1], bin1[2], bin1[3], bin2[0], bin2[1], bin2[2], bin2[3] };//On créé un nouveau tableau qui est 
                        //Constitué pour les 4 premiers index des 4 premières valeurs du tableau de l'image 1 et pour les 4 derniers index des 4
                        //premières valeurs du tableau de l'image 2. On onbtient ainsi une nouvelle valeur RVB en binaire qui aura majoritairement
                        //la couleur de l'image 1, mais qui sera un peu modifié par les couleurs du pixel de l'image 2 correspondant.
                        //On transforme ensuite ce tableau de 1 et 0 en entier, pour redonner sa valeur RVB
                        //au pixel de l'image représentant le codage.
                        byte couleur = bin_to_byte(bin3);
                        image3[i, j][k] = couleur;//On met donc la nouvelle valeur RVB dans le pixel de la nouvelle image
                    }
                }
            }
            return image3;
        }

        /// <summary>
        /// Méthode de décodage d'image : O prend l'image, on convertit chacun des pixels en séquence binaire. Puis on prend les 4 premiers 0 et 1 afin
        /// d'obtenir une première séquence que l'on complète avec des 0 pour obtenir une première image, puis on fait de même avec les 4 derniers 0 et 
        /// 1 pour obtenir une seconde image.
        /// </summary>
        /// <returns></returns>

        public byte[,][] decodage()
        {
            byte[,][] newimage = new byte[image.GetLength(0), image.GetLength(1)][];
            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    newimage[i, j] = new byte[3];
                }
            }
            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        byte[] bincouleur = byte_to_bin(image[i, j][k]);
                        byte[] newcouleur = { bincouleur[4], bincouleur[5], bincouleur[6], bincouleur[7], 0, 0, 0, 0 };
                        newimage[i, j][k] = bin_to_byte(newcouleur);
                    }
                }
            }
            return newimage;
        }



        /// <summary>
        /// METHODE DE CONVERSION BINAIRE/ENTIER
        /// Pour la conversion byte en binaire, on prend l'entier, on vérifie s'il est supérieur à 2^7.Si oui, on lui retranche 2^7 et on ajoute 1 à 
        ///l'index correspondant, sinon 0. On répète l'opération avec 2^6 et l'index 1, jusqu'à 2^0 et l'index 7. On obtient alors un tableau de 1 et 0
        ///qui est la traduction en binaire de l'entier
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>



        public byte[] byte_to_bin(byte val)
        {
            int val2=Convert.ToInt32(val);//On convertit le byte passé en paramètre en int pour effectuer des opération dessus
            byte[] binaire = new byte[8];//On créé le tableau binaire avec 8 index (car 8 index sont nécessaire pour traduire un nombre < à 256)
            if(val2>=128)//Si entier>2^7
            {
                binaire[0]++;//On ajoute 1 à l'index 0
                val2 = val2 - 128;
            }
            if(val2>=64)//Pareil avec 2^6
            {
                binaire[1]++;
                val2 = val2 - 64;
            }
            if(val2>=32)
            {
                binaire[2]++;
                val2 = val2 - 32;
            }
            if(val2>=16)
            {
                binaire[3]++;
                val2 = val2 - 16;
            }
            if (val2 >= 8)
            {
                binaire[4]++;
                val2 = val2 - 8;
            }
            if (val2 >= 4)
            {
                binaire[5]++;
                val2 = val2 - 4;
            }
            if (val2 >= 2)
            {
                binaire[6]++;
                val2 = val2 - 2;
            }
            if (val2 >= 1)
            {
                binaire[7]++;
            }
            return binaire;//On retourne le tableau de 1 et 0 
        }

        /// <summary>
        ///Pour la conversion binaire en entier, on multiplie simplement chaque index du tableau par sa puissance de 2 correspondante(index 7 2^0 index 0
        ///2^7)
        /// </summary>
        

        public byte bin_to_byte(byte[] bin)
        {
            int couleur = 0;
            int puissance2 = 1;
            for(int i=0;i<8;i++)
            {
                couleur = couleur + bin[7 - i] * puissance2;//On multiplie l'index avec sa puissance de 2 correspondante et on ajoute le résultat
                puissance2 = puissance2 * 2;//à la somme. On augmente d'une puissance de deux et on passe à l'index suivant
            }
            byte couleur2 = Convert.ToByte(couleur);//On convertit l'entier en byte pour pouvoir le mettre dans un pixel de l'image
            return couleur2;
        }
    }
}
