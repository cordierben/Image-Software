using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace Projet_Info
{
    public class MyImage
    {
        int taille;
        int offset;
        int largeur;
        int hauteur;
        int nbrecouleur;
        byte[] info;
        byte[] image;
        string file;


        public MyImage(string myfile1)
        {
            file = myfile1;
        }


        public byte[] Info
        { get { return info; } }
        public int Taille
        { get { return taille; } }
        public int Hauteur
        { get { return hauteur; } }
        public int Largeur
        { get { return largeur; } }
        public int Nbrecouleur
        { get { return nbrecouleur; } }
        public int Offset
        { get { return offset; } }
        public byte[] Image
        { get { return image; } }





        /// <summary>
        /// Méthode affichant les informations de l'image dans un fichier, et les mettant dans un tableau de bytes.
        /// On récupère le header, les infos du header et les infos de l'image.
        /// </summary>



        public void from_image_to_file()
        {
            info = File.ReadAllBytes(file);
            image = new byte[info.Length - 54];
            for (int i = 54; i < info.Length; i++)
            {
                image[i - 54] = info[i]; //On récupère les données RGB de l'image et on les isole dans un autre tableau
            }
            byte[] tabtaille = { info[2], info[3], info[4], info[5] };
            taille = convertir_endian_to_int(tabtaille);
            byte[] tablargeur = { info[18], info[19], info[20], info[21] };
            largeur = convertir_endian_to_int(tablargeur);
            byte[] tabhauteur = { info[22], info[23], info[24], info[25] };
            hauteur = convertir_endian_to_int(tabhauteur);
            byte[] tabcouleur = { info[28], info[29] };
            nbrecouleur = convertir_endian_to_int(tabcouleur);
            offset = 54;
        }



        /// <summary>
        /// Fonction recréant un tableau d'information permettant de retranscrire une image.
        /// </summary>
        /// <param name="matricePixel">est la matrice à transformer en image</param>
        /// <param name="hauteur">hauteur et largeur sont les dimensions de cette matrice</param>
        /// <param name="largeur"></param>
        /// <returns></returns>


        public byte[] sortie(byte[,][] matricePixel, int hauteur, int largeur)
        {
            ///info2 est le tableau avec les infos de la nouvelle image. Il est de taille égale au nombre de pixel*3 (car 3 couleurs pour chaque pixel)+54
            ///qui est la taille de l'en tête à remplir aussi!
            byte[] info2 = new byte[(hauteur * largeur * 3) + 54];
            byte[] hauteur1 = convertir_int_to_endian(hauteur);//On doit rentrer les dimensions dans l'en-tête de l'image, en endian
            byte[] largeur1 = convertir_int_to_endian(largeur);//On récupère donc les dimensions de la matrice passé en paramètre et on les rentredans info2
            byte[] taille1 = convertir_int_to_endian((hauteur * largeur * 3) + 54);//Là c'est la taille en octet du fichier (image+en tete) en endian
            byte[] tailleimage1 = convertir_int_to_endian(hauteur * largeur * 3);//Là c'est la taille en octet de l'image uniquement
            //A partir d'ici, on remplit le tableau avec les valeurs nécessaires : 
            info2[0] = 66;//Code pour .bmp 
            info2[1] = 77;//Pareil
            info2[10] = 54;//La valeur de l'offset, toujours 54 (pixel où commence l'image)
            info2[14] = 40;//Taille de l'en tete (toujours 40)
            info2[26] = 1;//toujours 1
            info2[28] = 24;//toujours 24

            for (int i = 0; i < 4; i++)//Ici on rentre les valeurs codées sur 4 octets en endian
            {
                info2[i + 22] = hauteur1[i];//La hauteur en endian
                info2[i + 18] = largeur1[i];//La largeur en endian
                info2[i + 2] = taille1[i];//La taille du fichier en endian
                info2[i + 34] = tailleimage1[i];//La taille de l'image en endian
                info2[i + 38] = info[i + 38];//La résolution qui est la même que l'image d'origine, donc on recopie les infos du tableau info de l'image d'origine
                info2[i + 42] = info[i + 42];//idem

            }
            int index = 0;
            for (int i = 0; i < matricePixel.GetLength(0); i++)
            {
                for (int j = 0; j < matricePixel.GetLength(1); j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        info2[54 + index] = matricePixel[i, j][k];//On rentre ensuite les infos des pixels RVB à partir de la matrice dans info2

                        index++;

                    }
                }
            }
            int a = 0;
            if (a == 1)
            {
                for (int i = 0; i < info2.Length; i++)
                {
                    Console.Write(info2[i] + " ");
                }
            }
            return info2;

        }


        /// <summary>
        /// Méthode identique à la précédente, différant uniquement pour la résolution sachant qu'on créé une image (utilisée pour les fractales)
        /// </summary>
        /// <param name="matricePixel"></param>
        /// <param name="hauteur"></param>
        /// <param name="largeur"></param>
        /// <returns></returns>
        public byte[] newsortie(byte[,][] matricePixel, int hauteur, int largeur)
        {
            byte[] info2 = new byte[(hauteur * largeur * 3) + 54];
            byte[] hauteur1 = convertir_int_to_endian(hauteur);
            byte[] largeur1 = convertir_int_to_endian(largeur);
            byte[] taille1 = convertir_int_to_endian((hauteur * largeur * 3) + 54);
            byte[] tailleimage1 = convertir_int_to_endian(hauteur * largeur * 3);
            info2[0] = 66;
            info2[1] = 77;
            info2[10] = 54;
            info2[14] = 40;
            info2[26] = 1;
            info2[28] = 24;

            for (int i = 0; i < 4; i++)
            {
                info2[i + 22] = hauteur1[i];
                info2[i + 18] = largeur1[i];
                info2[i + 2] = taille1[i];
                info2[i + 34] = tailleimage1[i];
                info2[i + 38] = 0;
                info2[i + 42] = 0;

            }
            int index = 0;
            for (int i = 0; i < matricePixel.GetLength(0); i++)
            {
                for (int j = 0; j < matricePixel.GetLength(1); j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        info2[54 + index] = matricePixel[i, j][k];

                        index++;

                    }
                }
            }
            return info2;
        }






        /// <summary>
        /// Convertit un tableau d'octet (bytes) en un tableau de int,
        /// en prenant les index des octets, et en les passant en int en multipliant par des puissances de 2.
        /// </summary>
        /// <param name="tab"></param>




        public int convertir_endian_to_int(byte[] tab)
        {
            int val=0;
            int puissance = 1;
            for(int i=0;i<tab.Length;i++)
            {
                val = val + tab[i] * puissance;
                puissance = puissance * 256;
            }
            return val;
        }




        /// <summary>
        /// Convertit un int en little endian, en réalisant des soustractions successives de 2 puissance 24,16,8 ou 0
        /// Place le nombre de soustractions réalisé pour chaque puissance dans un tableau de bytes.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>




        public byte[] convertir_int_to_endian(int val)
        {
            byte[] octet= { 0, 0, 0, 0 };
            int puissance24 = 16777216;
            int puissance16 = 65536;
            int puissance8 = 256;
            if(val>=puissance24)
            {
                int increment = 0;
                byte increment2 = 0;
                while (val>=puissance24)
                {
                    val = val - puissance24;
                    increment++;
                }
                increment2 = Convert.ToByte(increment);
                octet[3] = increment2;
            }
            if(val>=puissance16&&val<puissance24)
            {
                int increment = 0;
                byte increment2 = 0;
                while (val>=puissance16)
                {
                    val = val - puissance16;
                    increment++;
                }
                increment2 = Convert.ToByte(increment);
                octet[2] = increment2;
            }
            if(val>=puissance8&&val<puissance16)
            {
                int increment = 0;
                byte increment2 = 0;
                while (val>=puissance8)
                {
                    val = val - puissance8;
                    increment++;
                }
                increment2 = Convert.ToByte(increment);
                octet[1] = increment2;
            }
            if (val >= 0 && val < puissance8)
            {
                byte increment2=Convert.ToByte(val);
                octet[0] = increment2;
            }
            return octet;
        }
    }
}
