using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Info
{
    public class Manip
    {
        byte[,][] image;

        public Manip(byte[,][] image1)
        {
            image = image1;
        }

        public byte[,][] Image
        { get { return image; } }



        /// <summary>
        /// Méthode pour faire tourner une image à 90, 180, ou 270 degrés
        /// On réalise pour cela 1, 2, ou 3 rotations à 90° successives, en transposant l'image plusieurs fois ( (1,2) devient (2,1))
        /// </summary>
        /// <param name="val">valeur de la rotation</param>
        /// <returns></returns>
        

        public byte[,][] Rotation(int val)
        {
            byte[,][] newImage=null;
            byte[,][] newImage3 = null;

            if (val == 90||val==180||val==270)
            {
                newImage = new byte[image.GetLength(1), image.GetLength(0)][];
                for (int i = 0; i < newImage.GetLength(0); i++)
                {
                    for (int j = 0; j < newImage.GetLength(1); j++)
                    {
                        newImage[i, j] = new byte[3];
                    }
                }
                for (int i = 0; i < image.GetLength(0); i++)
                {
                    for (int j = 0; j < image.GetLength(1); j++)
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            newImage[j, newImage.GetLength(1) - 1 - i][k] = Image[i, j][k];
                        }

                    }
                }


                if (val == 180||val==270)
                {
                    byte[,][] newImage2 = new byte[newImage.GetLength(1), newImage.GetLength(0)][];
                    for (int i = 0; i < newImage2.GetLength(0); i++)
                    {
                        for (int j = 0; j < newImage2.GetLength(1); j++)
                        {
                            newImage2[i, j] = new byte[3];
                        }
                    }
                    for (int i = 0; i < newImage.GetLength(0); i++)
                    {
                        for (int j = 0; j < newImage.GetLength(1); j++)
                        {
                            for (int k = 0; k < 3; k++)
                            {
                                newImage2[j, newImage2.GetLength(1) - 1 - i][k] = newImage[i, j][k];
                            }

                        }

                    }


                    if (val == 270)
                    {
                        newImage3 = new byte[newImage2.GetLength(1), newImage2.GetLength(0)][];
                        for (int i = 0; i < newImage3.GetLength(0); i++)
                        {
                            for (int j = 0; j < newImage3.GetLength(1); j++)
                            {
                                newImage3[i, j] = new byte[3];
                            }
                        }
                        for (int i = 0; i < newImage2.GetLength(0); i++)
                        {
                            for (int j = 0; j < newImage2.GetLength(1); j++)
                            {
                                for (int k = 0; k < 3; k++)
                                {
                                    newImage3[j, newImage3.GetLength(1) - 1 - i][k] = newImage2[i, j][k];
                                }

                            }
                        }

                    }
                    else { newImage3 = newImage2; }
                }
                else { newImage3 = newImage; }
            }

            else
            {
                Console.WriteLine("La rotation n'est pas valide");
                newImage3 = Image;
            }
            return newImage3;

        }



        /// <summary>
        /// Méthode prenant la matrice de pixel voulue et renvoyant une nouvelle matrice de l'image en noir et blanc, en faisant la moyenne des RVB
        /// </summary>
        /// <returns></returns>


        public byte[,][] gris()
        {
            byte[,][] grise = image;
            for(int i=0;i<image.GetLength(0);i++)
            {
                for(int j=0;j<image.GetLength(1);j++)
                {
                    int somme = 0;
                    for(int k=0;k<3;k++)
                    {
                        somme = somme + image[i, j][k];
                    }
                    somme = somme / 3;
                    byte moyenne=Convert.ToByte(somme);
                    for(int h=0;h<3;h++)
                    {
                        grise[i, j][h] = moyenne;
                    }
                }
            }
            return grise;
        }

        /// <summary>
        /// Méthode passantl'image soit en noir, soit en blanc, en regardant si la somme des trois couleurs est plus proche de 0 ou de 3*255
        /// </summary>
        /// <returns></returns>

        public byte[,][] noirblanc()
        {
            byte[,][] noirblanc = image;
            for(int i=0;i<image.GetLength(0);i++)
            {
                for(int j=0;j<image.GetLength(1);j++)
                {
                    int somme = 0;
                    for(int k=0;k<3;k++)
                    {
                        somme += image[i, j][k];
                    }
                    if(somme<384)
                    {
                        for(int k=0;k<3;k++)
                        {
                            noirblanc[i, j][k] = 0;
                        }
                    }
                    else
                    {
                        for(int k=0;k<3;k++)
                        {
                            noirblanc[i, j][k] = 255;
                        }
                    }
                }
            }
            return noirblanc;
        }

        /// <summary>
        /// Méthode permettant d'appliquer un effet miroir, en inversant les coordonnées en ligne de l'image
        /// </summary>
        /// <returns></returns>

        public byte[,][] miroir()
        {
            byte[,][] miroir = new byte[image.GetLength(0), image.GetLength(1)][];
            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    miroir[i, j] = image[i, image.GetLength(1) - 1 - j];
                }
                
            }
            return miroir;
        }

        /// <summary>
        /// Méthode permettant d'agrandir une image à l'aide de l'interpolation, avec la possibilité de choisir le facteur d'agrandissement
        /// </summary>
        /// <returns></returns>

        public byte[,][] Agrandissement()
        {
            Console.WriteLine("Veuillez rentrer un rapport d'agrandissement (supérieur à 1) >");
            double rapport = Convert.ToDouble(Console.ReadLine());//On récupère le rapport d'agandissement entré par l'utilisateur ( qui peut être à virgule, donc double)
            int rapport2 = Convert.ToInt32(rapport * image.GetLength(0));//On multiplie ce rapport avec les dimensions de la matrice pour obtenir de nouvelles dimensions
            int rapport3 = Convert.ToInt32(rapport * image.GetLength(1));//et on les converties en int, car on peut pas rentrer de nombres à virgule en dimensions de matrice
            byte[,][] image2 = new byte[rapport2, rapport3][];//On rentre ici les nouvelles dimensions dans la nouvelle matrice
            for(int i=0;i<image2.GetLength(0);i++)
            {
                for(int j=0;j<image2.GetLength(1);j++)
                {
                    image2[i, j] = new byte[3];//On donne à chaque index de la nouvelle matrice un tableau de taille 3
                }
            }
            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    double i2 = rapport * i;//On multiplie chaque coordonnées (i,j) par le rapport de l'utilisateur, qu'on convertit en int car un index peut pas
                    double j2 = rapport * j;//être à virgule.Les index sont ainsi placés à intervalles réguliers, proportionellement aux nouvelles dimensions de la
                    int i3=Convert.ToInt32(i2);//matrice. Par exemple, pour un rapport de 4, les nouveaux index vont être placés toutes les 4 cases.
                    int j3=Convert.ToInt32(j2);
                    image2[i3, j3] = image[i, j];
                }
            }

            //Là commence la partie technique avec l'interpolation. On va analyser les cases créés une à une, et réaliser une interpolation dessus, c'est-à-dire qu'on
            //va analyser les 8 cases autour (rang 1), faire la moyenne de leur couleur, et l'envoyer sur la case d'origine. Si les 8 cases autour de la cases créés n'ont pas de valeur
            //car elles ont été aussi créés, ont élargis la recherche aux 16 cases autour (rang 2). Les modifications sont faites sur une matrice temporaire (tmp)
            //pour que les interpolations précédentes n'influent pas sur les suivantes. Puis on renvoit tout sur la matrice d'origine.

            byte[,][] tmp = image2;//On créé la matrice temporaire
            for (int i=0;i<image2.GetLength(0);i++)
            {
                for(int j=0;j<image2.GetLength(1);j++)//On parcourt tous les index de l'image avec les nouvelles dimensions un par un
                {
                    //Là se trouve le point faible de la méthode. Le if suivante permet de ne pas analyser les cases qui n'ont pas été créés, donc celle qui ont
                    //déjà une valeur. Cependant, une case qui vient d'être créés n'a pas la valeur null mais la valeur 0, donc noire. La méthode va donc analyser
                    //toutes les cases noires, y compris celle qui été déjà sur l'image.

                    if (image2[i, j][0] == 0&& image2[i, j][1] == 0&& image2[i, j][2] == 0)//rentre dans le if si la case est noire
                    {
                        int rang = 0;//Compteur permettant de donner le rang de la recherche autour de la case à analyser
                        int compteur = 0;//Compteur indiquant le nombre de cases dont on a fait la moyenne autour de la case analysée
                        int[] tab = new int[3];//Tableau temporaire RVB dans lequel on va faire l'interpolation pour chaque couleur, et dont on donnera les valeurs à la case
                        bool couleur = false;//Booléen disant si une case à été trouver lors de la recherche, et donc s'il faut passer au rang supérieur ou non
                        
                            while (couleur == false)//Tant qu'aucune case n'a été trouvé, on augmente d'un rang autour de la case
                            {
                                rang++;//On augmente le rang
                                int[] coordonnees = depassement(image2, i, j);//Méthode annexe gérant le problème lorsque le pixel est à l'extrémité de l'image (voir plus bas)
                                for (int i2 = i + rang*coordonnees[0]; i2 < i + rang*coordonnees[2]; i2++)//On parcours les cases autour en fonction des coordonnees renvoyés
                                {                                                                         //par la fonction dépassement.
                                    for (int j2 = j +  rang*coordonnees[1]; j2 < j +  rang*coordonnees[3]; j2++)
                                    {

                                    //Avec le if suivant, on regarde si l'image a une valeur ( et donc ne vient pas d'être créé). Si l'image a une valeur, on rentre dans le if
                                    //on ajoute sa valeur RVB au tableau temporaire, on augmente le compteur qui divisera la somme des couleurs pour en faire la moyenne, et on
                                    //passe le bool en true pour dire qu'une valeur a été trouvé, et qu'il ne sera pas utile de rechercher au rang supérieur.
                                        if (image2[i2, j2][0] != 0 || image2[i2, j2][1] != 0 || image2[i2, j2][2] != 0)
                                        {
                                            tab[0] = tab[0] + image2[i2, j2][0];
                                            tab[1] = tab[1] + image2[i2, j2][1];
                                            tab[2] = tab[2] + image2[i2, j2][2];
                                            couleur = true;
                                            compteur++;
                                        }

                                    }
                                }
                            }
                        
                        
                        for(int k=0;k<3;k++)
                        {
                            tmp[i, j][k] = Convert.ToByte(tab[k] / compteur);//On met la valeur du tableau temporaire dans la matrice temporaire ( en divisant par le compteur
                                                                             //pour faire une moyenne) et on convertit en byte (car matrice de byte, pas de int)
                        }

                    }
                }
            }
            for(int i=0;i<image2.GetLength(0);i++)
            {
                for(int j=0;j<image2.GetLength(1);j++)
                {
                    for(int k=0;k<3;k++)
                    {
                        image2[i, j][k] = tmp[i, j][k];//on renvoit toutes les valeurs de la matrice temporaire sur la nouvelle matrice d'image agrandie!
                    }
                }
            }
            return image2;
        }

        /// <summary>
        /// Méthode rétrécissant une image, avec possibilité de choisir le facteur. Cette méthode va fusionner des pixels entre eux, avec une sorte
        /// de matrice de convolution de taille variables
        /// </summary>
        /// <returns></returns>

        public byte[,][] retrecissement()
        {
            Console.WriteLine("Choisissez un facteur de rétrécissement (2, 4, 8 ou 16)>");
            double rapport = Convert.ToDouble(Console.ReadLine());
            int rapport2 = Convert.ToInt32(image.GetLength(0)/rapport);//Rapport 2 et rapport 3 seront les dimensions de la future matrice
            int rapport3 = Convert.ToInt32(image.GetLength(1)/rapport);
            while(rapport3%rapport!=0)//Si les dimensions ne sont pas un multiple du rapport, alors on les augmente, car on va rencontrer des problèmes
            {//de fusion des cases sinon
                rapport3++;
            }
            while(rapport2%rapport!=0)//idem
            {
                rapport2++;
            }
            int rapport4 = Convert.ToInt32(rapport);//On convertit le rapport en int (impossible de fusionner des pixels sinon)
            byte[,][] image2 = new byte[rapport2, rapport3][];//Création de la nouvelle matrice
            for (int i = 0; i < image2.GetLength(0); i++)
            {
                for (int j = 0; j < image2.GetLength(1); j++)
                {
                    image2[i, j] = new byte[3];//On donne à chaque index de la nouvelle matrice un tableau de taille 3
                    for(int k=0;k<3;k++)
                    {
                        image2[i, j][k] = 255;
                    }
                }
            }
            for(int i=0;i<image.GetLength(0);i=i+rapport4)
            {
                for(int j=0;j<image.GetLength(1);j=j+rapport4)//On parcours tous les N pixels de la matrice (N dépendant du rapport)
                {
                    int sommeR = 0;
                    int sommeV = 0;
                    int sommeB = 0;
                    int compteur = 0;
                    for(int i2=i;i2<i+rapport4;i2++)//Matrice de convolution dépendant du rapport, dans laquelle les cases vont être fusionnées
                    {
                        for(int j2=j;j2<j+rapport4;j2++)
                        {
                            if (i2 < image.GetLength(0) && j2 < image.GetLength(1))//On compte le nombre de cases analysées
                            {
                                compteur++;
                                sommeR = sommeR + image[i2, j2][0];//On fait la somme des couleurs
                                sommeV = sommeV + image[i2, j2][1];
                                sommeB = sommeB + image[i2, j2][2];
                            }
                           
                            
                        }
                    }
                    int a = i / rapport4;//Coordonnées de la case dans laquelle la nouvelle couleur va être attribuée
                    int b = j / rapport4;
                    
                    byte moyenneR = Convert.ToByte(sommeR / compteur);//On fait la moyenne des couleurs de chaque pixels
                    byte moyenneV = Convert.ToByte(sommeV / compteur);
                    byte moyenneB = Convert.ToByte(sommeB / compteur);
                    image2[a,b][0] = moyenneR;
                    image2[a,b][1] = moyenneV;//Et on donne la nouvelle couleur à la nouvelle image
                    image2[a,b][2] = moyenneB;
                }
            }    
            return image2;
        }

        /// <summary>
        /// Fonction permettant de gérer les bords de l'image. On retourne un tableau avec les valeurs initiales ci dessous. De base, si la case analysée n'est
        /// pas sur un bord de la matrice, on va, pour l'interpolation, analyser les 8 cases autour, donc les cases d'index -1 à 1 selon les lignes et -1 à 1
        /// selon les colonnes par rapport à la cases à analyser (donc de i-1 à i+1 et j-1 à j+1, faire un dessin pour comprendre plus facilement).
        ///  Mais si la case se trouve sur un bord, on va changer ces valeurs en fonction du bords pour ne pas se retrouver en dehors.
        /// </summary>
        /// <param name="tab"></param>matrice analysée
        /// <param name="a"></param>coordonnées x de la case analysée
        /// <param name="b"></param>coordonnées y de la case analysée
        /// <returns></returns>

        static int[] depassement(byte[,][] tab, int a,int b)
        {
            int[] coordonnees = { -1, -1, 1, 1 } ;//Valeurs par défaut
            if(a==0)//Si la case se trouve sur le bord supérieur...
            {
                coordonnees[0] = 0;//on analyse pas les cases se situant au dessus, donc de coordonnees i-1
            }
            if(b==0)//Si la case est sur le bord gauche...
            {
                coordonnees[1] = 0;//on analyse pas les cases à gauche, de coordonnees j-1
            }
            if(a==tab.GetLength(0)-1)//Si la case se trouve sur le bord inférieur...
            {
                coordonnees[2] = 0;//on analyse pas les cases en dessous, de coordonnees i+1
            }
            if (b == tab.GetLength(1) - 1)//Si la case est sur le bord de droite...
            {
                coordonnees[3] = 0;//On analyse pas les cases plus à droite, de coordonnées j+1
            }
            return coordonnees;

        }











        /// <summary>
        /// Méthode permettant de renforcer les bords, à l'aide d'une matrice de convolution : 
        ///  0 0 0
        /// -1 1 0
        ///  0 0 0
        ///  Le pixel devient noir s'il est situé sur le bord de l'image
        /// </summary>
        /// <returns></returns>


        public byte[,][] contours()
        {
            byte[,][] image2 = new byte[image.GetLength(0), image.GetLength(1)][];
            for(int i=0;i<image2.GetLength(0);i++)
            {
                for(int j=0;j<image2.GetLength(1);j++)
                {
                    image2[i, j] = new byte[3];
                }
            }
            for(int i=0;i<image.GetLength(0);i++)
            {
                for(int j=0;j<image.GetLength(1);j++)
                {
                    if (j == 0)
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            image2[i, j][k] = 0;
                        }
                    }
                    else
                    {
                        for(int k=0;k<3;k++)
                        {
                            int val = image[i, j][k] - image[i, j-1][k];
                            if(val<0)
                            {
                                val = 0;
                            }
                            byte val2 = Convert.ToByte(val);
                            image2[i, j][k] = val2;
                        }
                    }
                }
            }
            return image2;
        }










        /// <summary>
        /// Méthode permettant de détecter les bords avec une matrice de convolution : 
        /// 0  1  0
        /// 1 -4  1
        /// 0  1  0
        /// SI le pixel est sur le bords de l'image, il devient noir
        /// </summary>
        /// <returns></returns>


        public byte[,][] bords()
        {
            byte[,][] image2 = new byte[image.GetLength(0), image.GetLength(1)][];
            for (int i = 0; i < image2.GetLength(0); i++)
            {
                for (int j = 0; j < image2.GetLength(1); j++)
                {
                    image2[i, j] = new byte[3];
                }
            }
            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        if (i == 0 || j == 0||i==image.GetLength(0)-1||j==image.GetLength(1)-1)
                        {
                            image2[i, j][k] = 0;
                        }
                        else
                        {
                            int val = image[i - 1, j][k] + image[i, j - 1][k] + image[i + 1, j][k] + image[i, j + 1][k] - 4 * image[i, j][k];
                            if(val<0)
                            {
                                val = 0;
                            }
                            if(val>255)
                            {
                                val = 255;
                            }
                            byte val2 = Convert.ToByte(val);
                            image2[i, j][k] = val2;
                        }
                    }
                }
            }
            return image2;
        }















        /// <summary>
        /// Méthode permettant de repousser l'image avec la matrice de convolution :
        /// -2 -1  0
        /// -1  1  1
        ///  0  1  2
        ///  La méthode gère tous les cas de position du pixel en passant les coordonnées de la matrice de convolution à 0 s'ils sont en dehors
        /// </summary>
        /// <returns></returns>



        public byte[,][] Repoussage()
        {
            byte[,][] image2 = new byte[image.GetLength(0), image.GetLength(1)][];
            for (int i = 0; i < image2.GetLength(0); i++)
            {
                for (int j = 0; j < image2.GetLength(1); j++)
                {
                    image2[i, j] = new byte[3];
                }
            }
            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        int val = 0;
                        if(i==0)
                        {
                            if(j==0)
                            {
                                val = image[i, j][k] + image[i + 1, j][k] + image[i, j + 1][k] + 2 * image[i + 1, j + 1][k];
                            }
                            if(j==image.GetLength(1)-1)
                            {
                                val = - image[i, j - 1][k] + image[i, j][k] + image[i + 1, j][k];
                            }
                            if(j!=0&&j!=image.GetLength(1)-1)
                            {
                                val = - image[i, j - 1][k] + image[i, j][k] + image[i + 1, j][k] + image[i, j + 1][k] + 2 * image[i + 1, j + 1][k];
                            }
                        }
                        if(j==0)
                        {
                            if(i==0)
                            {
                                val = image[i, j][k] + image[i + 1, j][k] + image[i, j + 1][k] + 2 * image[i + 1, j + 1][k];
                            }
                            if(i==image.GetLength(0)-1)
                            {
                                val = - image[i - 1, j][k] + image[i, j][k]  + image[i, j + 1][k] ;
                            }
                            if(i!=0&&i!=image.GetLength(0)-1)
                            {
                                val = - image[i - 1, j][k] + image[i, j][k] + image[i + 1, j][k] + image[i, j + 1][k] + 2 * image[i + 1, j + 1][k];
                            }
                        }
                        if(i==image.GetLength(0)-1)
                        {
                            if(j==0)
                            {
                                val = -image[i - 1, j][k] + image[i, j][k] + image[i, j + 1][k];
                            }
                            if(j==image.GetLength(1)-1)
                            {
                                val = -2 * image[i - 1, j - 1][k] - image[i - 1, j][k] - image[i, j - 1][k] + image[i, j][k];
                            }
                            if(j!=0&&j!=image.GetLength(1)-1)
                            {
                                val = -2 * image[i - 1, j - 1][k] - image[i - 1, j][k] - image[i, j - 1][k] + image[i, j][k] + image[i, j + 1][k];
                            }
                        }
                        if(j==image.GetLength(1)-1)
                        {
                            if(i==0)
                            {
                                val = -image[i, j - 1][k] + image[i, j][k] + image[i + 1, j][k];
                            }
                            if(i==image.GetLength(0)-1)
                            {
                                val = -2 * image[i - 1, j - 1][k] - image[i - 1, j][k] - image[i, j - 1][k] + image[i, j][k];
                            }
                            if(i!=0&&i!=image.GetLength(0)-1)
                            {
                                val = -2 * image[i - 1, j - 1][k] - image[i - 1, j][k] - image[i, j - 1][k] + image[i, j][k] + image[i + 1, j][k];
                            }
                        }
                        if (i != 0 && i != image.GetLength(0) - 1 && j != 0 && j != image.GetLength(1)-1)
                        {
                            val = -2 * image[i - 1, j - 1][k] - image[i - 1, j][k] - image[i, j - 1][k] + image[i, j][k] + image[i + 1, j][k] + image[i, j + 1][k] + 2 * image[i + 1, j + 1][k];
                        }
                        if (val < 0)
                        {
                            val = 0;
                        }
                        if (val > 255)
                        {
                            val = 255;
                        }
                        byte val2 = Convert.ToByte(val);
                        image2[i, j][k] = val2;
                    }
                }
            }
            return image2;
        }




        /// <summary>
        /// Méthode appliquant un flou avec la matrice de convolution : 
        /// 1 1 1
        /// 1 1 1
        /// 1 1 1
        /// La méthode gère toutes les positions possible du pixel
        /// </summary>
        /// <returns></returns>

        public byte[,][] Flou()
        {
            byte[,][] image2 = new byte[image.GetLength(0), image.GetLength(1)][];

            for (int i = 0; i < image2.GetLength(0); i++)
            {
                for (int j = 0; j < image2.GetLength(1); j++)
                {
                    image2[i, j] = new byte[3];
                }
            }

            for (int i = 0; i < image2.GetLength(0); i++)
            {
                for (int j = 0; j < image2.GetLength(1); j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        int val = 0;
                        if (i == 0)
                        {
                            if (j == 0)
                            {
                                val = (image[i, j][k] + image[i, j + 1][k] + image[i + 1, j + 1][k] + image[i + 1, j][k]) / 4;
                            }
                            if (j == image.GetLength(1) - 1)
                            {
                                val = (image[i, j][k] + image[i, j - 1][k] + image[i + 1, j - 1][k] + image[i + 1, j][k]) / 4;
                            }
                            if (j != 00 && j != image.GetLength(1) - 1)
                            {
                                val = (image[i, j][k] + image[i, j - 1][k] + image[i, j + 1][k] + image[i + 1, j][k] + image[i + 1, j - 1][k] + image[i + 1, j + 1][k]) / 6;
                            }
                        }

                        if (j == 0)
                        {
                            if (i == image.GetLength(0) - 1)
                            {
                                val = (image[i, j][k] + image[i, j + 1][k] + image[i - 1, j + 1][k] + image[i - 1, j][k]) / 4;
                            }
                            if (i != 00 && i != image.GetLength(0) - 1)
                            {
                                val = (image[i, j][k] + image[i - 1, j][k] + image[i + 1, j][k] + image[i, j + 1][k] + image[i - 1, j + 1][k] + image[i + 1, j + 1][k]) / 6;
                            }
                        }

                        if (i == image.GetLength(0) - 1)
                        {
                            if (j == image.GetLength(1) - 1)
                            {
                                val = (image[i, j][k] + image[i, j - 1][k] + image[i - 1, j - 1][k] + image[i - 1, j][k]) / 4;
                            }
                            if (j != image.GetLength(1) - 1 && j != 0)
                            {
                                val = (image[i, j][k] + image[i, j - 1][k] + image[i, j + 1][k] + image[i - 1, j - 1][k] + image[i - 1, j][k] + image[i - 1, j + 1][k]) / 6;
                            }
                        }
                        if (j == image.GetLength(1) - 1 && i != 0 && i != image.GetLength(0) - 1)
                        {
                            val = (image[i, j][k] + image[i - 1, j][k] + image[i + 1, j][k] + image[i - 1, j - 1][k] + image[i, j - 1][k] + image[i + 1, j - 1][k]) / 6;
                        }

                        if (i != 0 && j != 0 && i != image.GetLength(0) - 1 && j != image.GetLength(1) - 1)
                        {
                            val = (image[i - 1, j - 1][k] + image[i, j - 1][k] + image[i + 1, j - 1][k] + image[i - 1, j][k] + image[i, j][k] + image[i + 1, j][k] + image[i - 1, j + 1][k] + image[i, j + 1][k] + image[i + 1, j + 1][k]) / 9;
                        }
                        byte val2 = Convert.ToByte(val);
                        image2[i, j][k] = val2;
                    }
                }
            }
            return image2;
        }



        /// <summary>
        /// Méthode appliquant une augmentation du contraste avec la matrice de convolution : 
        /// 0  -1   0
        ///-1   5  -1
        /// 0  -1   0
        /// METHODE BONUS NON DEMANDEE
        /// </summary>
        /// <returns></returns>



        public byte[,][] Contraste()
        {
            byte[,][] image2 = new byte[image.GetLength(0), image.GetLength(1)][];
            for (int i = 0; i < image2.GetLength(0); i++)
            {
                for (int j = 0; j < image2.GetLength(1); j++)
                {
                    image2[i, j] = new byte[3];
                }
            }
            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        if (i == 0 || j == 0 || i == image.GetLength(0) - 1 || j == image.GetLength(1) - 1)
                        {
                            image2[i, j][k] = 0;
                        }
                        else
                        {
                            int val = -image[i - 1, j][k] - image[i, j - 1][k] + 5 * image[i, j][k] - image[i + 1, j][k] - image[i, j + 1][k]; 
                            if (val < 0)
                            {
                                val = 0;
                            }
                            if (val > 255)
                            {
                                val = 255;
                            }
                            byte val2 = Convert.ToByte(val);
                            image2[i, j][k] = val2;
                        }
                    }
                }
            }
            return image2;
        }



        /// <summary>
        /// Méthode créant les histogrammes RVB de l'image. Cette méthode va être appelée 3 fois, une fois par histogramme.
        /// On va compter la proportion de chaque teintes d'une des trois couleurs par rapport aux autres. On met alors cette proportion sous forme
        /// de barres dans l'histogramme, un pour chaque nuances (donc 256 barres).
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>

        public byte[,][] histogramme(int k)
        {
            byte[,][] newimage = new byte[256*3, 256][];//On créé une nouvelle matrice avec des dimensions bien précises pour que l'histogramme soit visible
            for(int i=0;i<256*3;i++)
            {
                for(int j=0;j<256;j++)
                {
                    newimage[i, j] = new byte[3];
                    for(int blanc=0;blanc<3;blanc++)
                    {
                        newimage[i, j][blanc] = 255;
                    }
                }
            }
            for(int couleur=0;couleur<256;couleur++)
            {
                int compteur = 0;
                for (int i = 0; i < image.GetLength(0); i++)
                {
                    for (int j = 0; j < image.GetLength(1); j++)
                    {

                        if (image[i, j][k] == couleur)
                        {
                            compteur++;//On compte le nombre de fois que la nuance est présente dans l'histogramme
                        }
                    }
                }
                byte couleur2 = Convert.ToByte(couleur);
                byte compteur2 = Convert.ToByte(Convert.ToInt32((compteur*255) / (image.GetLength(0) * image.GetLength(1))));
                for (int CouleurX = 0; CouleurX < 3*compteur2 + 1; CouleurX=CouleurX+3)
                {
                    for (int abscisse = 0; abscisse < 3; abscisse++)//On dessine l'histogramme en fonction de sa nuance et de la proportion de la nuance
                    {
                        newimage[CouleurX + abscisse, couleur][0] = 0;
                        newimage[CouleurX + abscisse, couleur][1] = 0;
                        newimage[CouleurX + abscisse, couleur][2] = 0;
                        newimage[CouleurX + abscisse, couleur][k] = couleur2;
                      
                    }
                }   
                
            }
            return newimage;
            
        }
    }
}
