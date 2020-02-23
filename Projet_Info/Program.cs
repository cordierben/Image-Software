using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace Projet_Info
{
    class Program
    {
        /// <summary>
        /// Menu principal codé en dur dans le main, permettant de choisir le menu désiré et de modifier ou créer une image selon les classes
        /// </summary>
        /// <param name="args"></param>
        
        static void Main(string[] args)
        {
            ConsoleKeyInfo cki;
                do
                {
                    Console.Clear();
                    Console.WriteLine("Bienvenue dans le logiciel de traitement d'images de Paul Carayon et Benoît Cordier!");
                    Console.WriteLine();
                    Console.WriteLine("          Menu Principal ");//L'utilisateur choisit ici un menu du logiciel
                    Console.WriteLine("Partie 1: Manipulation d'une image");
                    Console.WriteLine("Partie 2: Création d'une fractale");
                    Console.WriteLine("Partie 3: Traitement d'une image");
                    Console.WriteLine();
                    Console.WriteLine("Sélectionnez la partie desirée");
                    int exo = SaisieNombre();
                    switch (exo)//switch en fonction du numéro d'exo voulu
                    {
                        #region
                        case 1://Cas 1 : Manipulation d'image
                            do
                            {

                                Console.WriteLine("Saisissez le nom de l'image (exemple : coco.bmp, lac_en_montagne.bmp,...)");
                                Console.WriteLine("Tapez sortie.bmp pour l'image avec la dernière manipulation");
                                string nom = Convert.ToString(Console.ReadLine());//L'utilisateur choisit l'image qu'il souhaite manipuler
                                MyImage image = new MyImage(nom);//Le nom de l'image est passé en objet de la classe MyImage
                                Process.Start(nom);//Affichage de l'image
                                image.from_image_to_file();//Traduction de l'image en tableau d'octet

                                Matrice image1 = new Matrice(image.Image);//Le tableau d'octet est passé en objet de la classe Matrice
                                image1.pixel();//Le tableau d'octet devient un tableau de pixel...
                                image1.matricepixel(image.Hauteur, image.Largeur);//...puis une matrice de pixel

                                Manip image2 = new Manip(image1.MatricePixel);//La matrice de pixel devient un objet de la classe Manip

                                Console.Clear();
                                Console.WriteLine("Menu Manipulation :\n");//Sélection de la manipulation souhaitée
                                Console.WriteLine("Manipulation 1 : Rotation");
                                Console.WriteLine("Manipulation 2 : Effet Miroir");
                                Console.WriteLine("Manipulation 3 : Teintes de gris");
                                Console.WriteLine("Manipulation 4 : Agrandir l'image");
                                Console.WriteLine("Manipulation 5 : Rétrécir l'image");
                                Console.WriteLine("Manipulation 6 : Renforcer les contours de l'image");
                                Console.WriteLine("Manipulation 7 : Détection des contours de l'image");
                                Console.WriteLine("Manipulation 8 : Repoussage de l'image");
                                Console.WriteLine("Manipulation 9 : Flouter l'image");
                                Console.WriteLine("Manipulation 10 :Noir ou Blanc ");
                                Console.WriteLine("Manipulation 11 (bonus) : Augmenter le contraste");
                                Console.WriteLine();
                                Console.WriteLine("Sélectionnez l'exercice désiré ");
                                int exo2 = SaisieNombre();
                                switch (exo2)//switch en fonction de la manipulation souhaitée
                                {
                                    #region
                                    case 1:
                                        Console.WriteLine("Choisissez une rotation (90,180 ou 270 degrés)");
                                        int val = Convert.ToInt32(Console.ReadLine());
                                        Manip Rotation = new Manip(image2.Rotation(val));//La matrice passe dans la méthode rotation avec la valeur d'angle
                                        byte[] tabRotation = image.sortie(Rotation.Image, Rotation.Image.GetLength(0), Rotation.Image.GetLength(1));
                                        File.WriteAllBytes("Sortie.bmp", tabRotation);//La matrice modifiée est renvoyée et convertit en tab d'octet
                                        Process.Start("Sortie.bmp");//La nouvelle image est affichée
                                        break;
                                    case 2:
                                        Manip Miroir = new Manip(image2.miroir());//idem que la rotation avec la méthode miroir
                                        byte[] tabmiroir = image.sortie(Miroir.Image, Miroir.Image.GetLength(0), Miroir.Image.GetLength(1));
                                        File.WriteAllBytes("Sortie.bmp", tabmiroir);
                                        Process.Start("Sortie.bmp");
                                        break;
                                    case 3:
                                        Manip Gris = new Manip(image2.gris());//idem que la rotation avec la méthode gris
                                    byte[] tabgris = image.sortie(Gris.Image, Gris.Image.GetLength(0), Gris.Image.GetLength(1));
                                        File.WriteAllBytes("Sortie.bmp", tabgris);
                                        Process.Start("Sortie.bmp");
                                        break;

                                    case 4:
                                        Manip Agrandir = new Manip(image2.Agrandissement());//idem que la rotation avec la méthode agrandissement
                                    byte[] tabagrandir = image.sortie(Agrandir.Image, Agrandir.Image.GetLength(0), Agrandir.Image.GetLength(1));
                                        File.WriteAllBytes("Sortie.bmp", tabagrandir);
                                        Process.Start("Sortie.bmp");
                                        break;
                                    case 5:
                                        Manip Retrecir = new Manip(image2.retrecissement());//idem que la rotation avec la méthode rétrécissement
                                    byte[] tabretrecir = image.sortie(Retrecir.Image, Retrecir.Image.GetLength(0), Retrecir.Image.GetLength(1));
                                        File.WriteAllBytes("Sortie.bmp", tabretrecir);
                                        Process.Start("Sortie.bmp");
                                        break;
                                    case 6:
                                        Manip Contours = new Manip(image2.contours());//idem que la rotation avec la méthode contour
                                    byte[] tabContours = image.sortie(Contours.Image, Contours.Image.GetLength(0), Contours.Image.GetLength(1));
                                        File.WriteAllBytes("Sortie.bmp", tabContours);
                                        Process.Start("Sortie.bmp");
                                        break;
                                    case 7:
                                        Manip Bords = new Manip(image2.bords());//idem que la rotation avec la méthode bords
                                    byte[] tabBords = image.sortie(Bords.Image, Bords.Image.GetLength(0), Bords.Image.GetLength(1));
                                        File.WriteAllBytes("Sortie.bmp", tabBords);
                                        Process.Start("Sortie.bmp");
                                        break;
                                    case 8:
                                        Manip Repoussage = new Manip(image2.Repoussage());//idem que la rotation avec la méthode repoussage
                                    byte[] tabrepoussage = image.sortie(Repoussage.Image, Repoussage.Image.GetLength(0), Repoussage.Image.GetLength(1));
                                        File.WriteAllBytes("Sortie.bmp", tabrepoussage);
                                        Process.Start("Sortie.bmp");
                                        break;
                                    case 9:
                                        Manip Flou = new Manip(image2.Flou());//idem que la rotation avec la méthode flou
                                    byte[] tabFlou = image.sortie(Flou.Image, Flou.Image.GetLength(0), Flou.Image.GetLength(1));
                                        File.WriteAllBytes("Sortie.bmp", tabFlou);
                                        Process.Start("Sortie.bmp");
                                        break;
                                    case 10:
                                        Manip NoirBlanc = new Manip(image2.noirblanc());//idem que la rotation avec la méthode noirblanc
                                    byte[] tabnoirblanc = image.sortie(NoirBlanc.Image, NoirBlanc.Image.GetLength(0), NoirBlanc.Image.GetLength(1));
                                        File.WriteAllBytes("sortieNoirBlanc.bmp", tabnoirblanc);
                                        Process.Start("sortieNoirBlanc.bmp");
                                        break;
                                    case 11:
                                        Manip Contraste = new Manip(image2.Contraste());//idem que la rotation avec la méthode contraste
                                    byte[] tabContraste = image.sortie(Contraste.Image, Contraste.Image.GetLength(0), Contraste.Image.GetLength(1));
                                        File.WriteAllBytes("Sortie.bmp", tabContraste);
                                        Process.Start("Sortie.bmp");
                                        break;

                                    default: break;
                                        #endregion
                                }
                                Console.WriteLine("Tapez Escape pour sortir ou espace pour rechoisir une manipulation");
                                cki = Console.ReadKey();
                            } while (cki.Key != ConsoleKey.Escape);//Tant que l'utilisateur n'a pas tapé escape, on reste dans le menu manipulation
                            break;//Sinon, retour au menu principal
                        case 2://case 2 : partie Fractale
                            do
                            {
                                Console.Clear();
                                Console.WriteLine("Fractale 1 : Dessiner la fractale de Mandelbrot");
                                Console.WriteLine("Fractale 2 : Dessiner la fractale de Julia (dessiner sa propre fractale!) CREATION BONUS TD5");
                                Console.WriteLine();
                                Console.WriteLine("Sélectionnez la fractale désirée>");//Choix de la fractale
                                int exo3 = SaisieNombre();
                                switch (exo3)
                                {
                                    case 1:
                                        string nouveau="";//nom d'image inexistant, afin de créer un objet dans la classe MyImage, pour pouvoir convertir
                                        MyImage imagefrac = new MyImage(nouveau);//en bitmap la mtrice qui sera créée
                                        Console.WriteLine("Veuillez saisir les dimensions de l'image contenant la fractale(500 ou 1000)>");
                                        int taille = Convert.ToInt32(Console.ReadLine());
                                        byte[,][] TmpFractale = new byte[taille, taille][];//Création d'une matrice temporaire qui subira les modifs
                                        Fractale frac1 = new Fractale(TmpFractale);//la matrice temp devient un objet de la classe fractale
                                        byte[,][] fractale = frac1.Mandelbrot(taille);//Une nouvelle matrice réupère les modifications de la matrice temp
                                        byte[] tabFractale = imagefrac.newsortie(fractale, fractale.GetLength(0), fractale.GetLength(1));
                                        File.WriteAllBytes("SortieFractaleMandelbrot.bmp", tabFractale);//la nouvelle matrice est convertit en tab d'octet
                                        Process.Start("SortieFractaleMandelbrot.bmp");//La fractale est affichée
                                        break;
                                    case 2:
                                        string nouveau2 = "";
                                        MyImage imagefrac2 = new MyImage(nouveau2);//idem que pour la fractale de Mandelbrot
                                        Console.WriteLine("Veuillez saisir les dimensions de l'image contenant la fractale(500 ou 1000)>");
                                        int taille2 = Convert.ToInt32(Console.ReadLine());
                                        byte[,][] TmpFractale2 = new byte[taille2, taille2][];
                                        Fractale frac2 = new Fractale(TmpFractale2);
                                        byte[,][] fractale2 = frac2.Julia(taille2);
                                        byte[] tabFractale2 = imagefrac2.newsortie(fractale2, fractale2.GetLength(0), fractale2.GetLength(1));
                                        File.WriteAllBytes("SortieFractaleJulia.bmp", tabFractale2);
                                        Process.Start("SortieFractaleJulia.bmp");
                                        break;
                                    default:break;
                                        


                                }
                                Console.WriteLine("Tapez Escape pour revenir au menu principal, ou espace pour rechoisir une fractale");
                                cki = Console.ReadKey();
                            }while(cki.Key != ConsoleKey.Escape);//Tant que l'utilisateur n'a pas tapé escape, on reste dans le menu fractale
                        break;//Sinon, retour au menu principal
                    case 3://case 3 : Menu traitement
                            do
                            {

                                

                                Console.Clear();
                                Console.WriteLine("Menu Traitement :\n");
                                Console.WriteLine("Traitement 1 : Histogramme RVB d'une image");
                                Console.WriteLine("Traitement 2 : Codage et décodage d'une image");
                                Console.WriteLine();
                                Console.WriteLine("Sélectionnez l'exercice désiré ");//Choix du traitement
                                int exo2 = SaisieNombre();
                                switch (exo2)
                                {
                                    #region
                                    case 1:
                                        Console.WriteLine("Saisissez le nom de l'image (exemple : coco.bmp, lac_en_montagne.bmp,...)>");
                                        string nom = Convert.ToString(Console.ReadLine());
                                        MyImage image = new MyImage(nom);//Idem que pour le menu manipulation
                                        Process.Start(nom);
                                        image.from_image_to_file();

                                        Matrice image1 = new Matrice(image.Image);
                                        image1.pixel();
                                        image1.matricepixel(image.Hauteur, image.Largeur);

                                        Manip image2 = new Manip(image1.MatricePixel);

                                        for (int k = 0; k < 3; k++)//On fait un compteur pour chaque couleur RVB, afin de créer les 3 histogrammes
                                        {
                                            Manip Histogramme = new Manip(image2.histogramme(k));//Passe la matrice en objet de Manip en fonction de la couleur
                                            byte[] tabHisto = image.sortie(Histogramme.Image, Histogramme.Image.GetLength(0), Histogramme.Image.GetLength(1));
                                            File.WriteAllBytes("Sortie"+k+".bmp", tabHisto);//Affiche l'histogramme de la couleur correspondante
                                            Process.Start("Sortie"+k+".bmp");
                                            
                                        }
                                        break;
                                    case 2:
                                        do
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Manipulation 1 : Coder une image dans une autre");
                                            Console.WriteLine("Manipulation 2 : Décoder une image");
                                            Console.WriteLine();
                                            Console.WriteLine("Saisissez le numéro de l'exercice souhaité");//Sélection du codage ou décodage
                                            int exo3 = SaisieNombre();
                                            switch(exo3)
                                            {
                                                case 1:
                                                    Console.WriteLine("Choisissez l'image à coder");
                                                    string nomimage1 = Convert.ToString(Console.ReadLine());
                                                    Console.WriteLine("Choisissez l'image dans laquelle vous voulez la coder");
                                                    string nomimage2 = Convert.ToString(Console.ReadLine());
                                                    MyImage imagecode = new MyImage(nomimage2);//On convertit la première image en tableau d'octet
                                                    Process.Start(nomimage2);
                                                    imagecode.from_image_to_file();


                                                    Matrice image1code = new Matrice(imagecode.Image);
                                                    image1code.pixel();//On transforme le tableau d'octet en mat de pixel
                                                    image1code.matricepixel(imagecode.Hauteur, imagecode.Largeur);

                                                    MyImage image2code = new MyImage(nomimage1);//Et on fait la même chose pour la seconde image
                                                    Process.Start(nomimage1);
                                                    image2code.from_image_to_file();

                                                    Matrice image3 = new Matrice(image2code.Image);
                                                    image3.pixel();
                                                    image3.matricepixel(image2code.Hauteur, image2code.Largeur);

                                                    Codage image4 = new Codage(image1code.MatricePixel);//On passe la 1ere image en objet de Codage
                                                    Codage image5 = new Codage(image4.codage(image3.MatricePixel));//Et on créé un objet Codage avec le premier objet, et la seconde mat de pixel caché
                                                    byte[] tabcodage = imagecode.sortie(image5.Image, image5.Image.GetLength(0), image5.Image.GetLength(1));
                                                    File.WriteAllBytes("sortieCodage.bmp", tabcodage);//On convertit la mat de pixel des deux images et on affiche
                                                    Process.Start("sortieCodage.bmp");
                                                    break;
                                                case 2://décodage
                                                    Console.WriteLine("Choisissez que vous voulez décoder (s'il y en a une, tapez sortieCodage.bmp, cela décodera la dernière image codée)");
                                                    string nomimage = Convert.ToString(Console.ReadLine());
                                                    MyImage imagedecodage = new MyImage(nomimage);//On passe l'image codée en objet de MyImage
                                                    Process.Start(nomimage);
                                                    imagedecodage.from_image_to_file();


                                                    Matrice image1decodage = new Matrice(imagedecodage.Image);
                                                    image1decodage.pixel();//On la convertit en matrice de pixel
                                                    image1decodage.matricepixel(imagedecodage.Hauteur, imagedecodage.Largeur);

                                                    Codage image2decodage = new Codage(image1decodage.MatricePixel);//On passe la mat de pixel en objet de codage
                                                    Codage decodage2 = new Codage(image2decodage.decodage());//Puis on créé un second objet, représentant le décodage des deux images codées
                                                    byte[] tabdecodage = imagedecodage.sortie(decodage2.Image, decodage2.Image.GetLength(0), decodage2.Image.GetLength(1));
                                                    File.WriteAllBytes("Sortiedecodage.bmp", tabdecodage);
                                                    Process.Start("sortiedecodage.bmp");//On convertit et on affiche l'image cachée
                                                    break;

                                            }
                                            Console.WriteLine("Tapez Escape pour sortir ou espace pour coder/décoder une autre image");
                                            cki = Console.ReadKey();
                                        } while (cki.Key != ConsoleKey.Escape);
                                        break;


                                    default: break;
                                        #endregion
                                }
                                Console.WriteLine("Tapez Escape pour sortir ou espace pour rechoisir un traitement");
                                cki = Console.ReadKey();
                            } while (cki.Key != ConsoleKey.Escape);
                            break;
                        default:break;
                            #endregion
                    }
                    Console.WriteLine("Tapez Escape pour quitter le logiciel, ou espace pour revenir au menu principal");
                    cki = Console.ReadKey();
                } while (cki.Key != ConsoleKey.Escape);
            
        }

        /// <summary>
        /// Méthode permettant de choisir le numéro d'exercice
        /// </summary>
        /// <returns></returns>

        static int SaisieNombre()
        {
            int nbre= Convert.ToInt32(Console.ReadLine());
            return nbre;
        }


    }
}

