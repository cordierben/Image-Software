using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Info
{
    class Matrice
    {
        byte[] image;
        byte[][] imagePixel;
        byte[,][] matricePixel;
        

        public Matrice(byte[] image1)
        {
            image = image1;
        }


        public byte[] Image
        { get { return image; } }
        public byte[][] ImagePixel
        { get { return imagePixel; } }
        public byte[,][] MatricePixel
        { get { return matricePixel; } }


        /// <summary>
        /// On modifie dans cette méthode le tableau de pixel en un tableau de tableau avec :
        /// Dans les tableaux les pixels un par index
        /// Dans les sous tableaux les couleurs RVB de chaque pixel
        /// </summary>

        public void pixel()
        {
            imagePixel = new byte[(image.Length / 3)][];
            for(int i=0; i<imagePixel.Length;i++)
            {
                imagePixel[i] = new byte[3];
            }
            int index = 0;
            for (int i = 0; i < image.Length; i = i + 3)
            {
                for (int j = 0; j < 3; j++)
                {
                    imagePixel[index][j] = image[i + j];
                }
                index++;
            }
        }


        /// <summary>
        /// On modifie ici le tableau de tableau en une  matrice de tableau pour pouvoir effectuer des manipulations dessus
        /// On doit donc récupérer les dimensions de l'image pour avoir une matrice de même taille.
        /// </summary>
        /// <param name="hauteur"></param>
        /// <param name="largeur"></param>

        public void matricepixel(int hauteur, int largeur)
        {
            Console.WriteLine();
            matricePixel = new byte[hauteur, largeur][];
            for (int i = 0; i < matricePixel.GetLength(0); i++)
            {
                for (int j = 0; j < matricePixel.GetLength(1); j++)
                {
                    matricePixel[i, j] = new byte[3];
                }
            }
            int index = 0;
            for (int i = 0; i < matricePixel.GetLength(0); i++)
            {
                for (int j = 0; j < matricePixel.GetLength(1); j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        matricePixel[i, j][k] = imagePixel[index][k];
                    }
                    index++;
                }
                
            }   
        }
    }
}
