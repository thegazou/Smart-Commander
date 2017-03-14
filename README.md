# Smart-Commander

## Situation initiale
Il existe plusieurs implémentations de l’explorateur Windows. Cependant, aucun ne propose une interface simple allié à une puissante API de renommage de nom de fichier en masse.

## Objectifs principaux
* Les composants de la fenêtre encapsulant les explorateurs de fichiers s’ajustent dynamiquement en fonction de la taille de la fenêtre.
* Séparation modulable de l’interface en deux explorateurs indépendants
  * Un explorateur source et un explorateur cible
  * Liste des dossiers sources/cibles récemment utilisés
* Affichage de l’arborescence de dossier sous forme d’arbre
* Navigation dans les dossiers de manière relative et absolue
** Copier/couper-coller de fichiers de l’explorateur source à l’explorateur cible
  * Avec un bouton
  * Avec un seul raccourcis clavier
* Menu contextuel Windows accessible avec un clic droit sur un élément de l’explorateur
* Exécution d’un fichier avec un double clique gauche sur un fichier de l’éléments de l’explorateur
* Renommage de fichier en groupe
   * Définition du pattern de renommage par l’utilisateur à l’aide :
     * De regex
     * De générateur de regex


## Objectifs secondaires
* Undo

## Contraintes
* Développement en C# avec une interface WPF
