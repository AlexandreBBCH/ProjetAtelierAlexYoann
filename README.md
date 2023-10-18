# Projet C# Monogame : Forest Survivor

## Auteur
Yoann Meier et Alexandre Babiche
## Période
05/09/2023 au 17/10/2023
## Objectif
L’objectif de ce projet est de développer une application Monogame en utilisant C# qui exploite des Sprites pour créer une expérience interactive. <br>
L’application sera conçue pour créer un jeu 2D vue du dessus. Le but du jeu est d’affronter des vagues d'ennemis dans divers niveaux.
## Fonctionnalités Principales
 
### Fonctionnalité 1
Un personnage qui se déplace dans toutes les directions :
- W pour monter
- A pour aller à gauche
- S pour descendre
- D pour aller à droite
On peut également aller en diagonale en appuyant sur 2 touches (W et D pour aller en haut à droite par exemple).<br>
Pour chaque direction, un sprite a été créé. Tous ces sprites seront stockés dans une liste de Texture2D pour facilement changer de sprite.
### Fonctionnalité 2
Le personnage tire des projectiles jusqu’au mur dans la même direction que le personnage. Le tir disparaît lors de la collision.<br>
Néanmoins, si le tir touche un ennemi avant le mur, le tir disparaît et le nombre de point de vie du monstre baisse selon les dégâts infligés par le joueur et l’ennemi disparaît si ses points de vie sont égal à 0.
### Fonctionnalité 3
Collision entre le personnage et les éléments du décors.<br> 
Lorsque le personnage touche le bord de l'écran ou un obstacle, il ne peut plus avancer et passer à travers.
### Fonctionnalité 4
Il y aura des bruitages pour donner plus de vie au jeu. <br>
Un bruitage lors du tir, lorsqu’un ennemi est touché, lorsque le joueur perd la partie.
### Fonctionnalité 5
Il y a un menu au lancement du jeu avec :
- Jouer : Nous permet de lancer une nouvelle partie
- Option : Permet de changer le volume de la musiques et des bruitages avec un sliders
- Quitter : Quitte le jeu quand cliqué dessus
### Fonctionnalité 6
Il y aura menu option qui sert à gérer le volume du son des musiques et des bruitages.
### Fonctionnalité 7
Il y aura 3 ennemis qui auront des points de vie ou une vitesse différente
- Slime : Ennemie de base qui traque le joueur et lui fait perdre de la vie au contact
- Slime tireur : Un slime qui tire à distance sur le joueur
- Slime Boss : Plus grand et se divise en slime de base à la mort
### Fonctionnalité 8
Les ennemies se rapproche du joueur sans traverser les obstacles
### Fonctionnalité 9
Il y aura plusieurs objets qui pourront aider le joueur : invincibilité, bonus de vitesse …<br>
Certains seront même permanents.
- Pomme : Restauration d’un nombre pv (à définir)
- Champignon : Augmente la vitesse de manière permanente de 2% (se cumule)
- De l’huile : Pour augmenter la cadence de tir définitivement de 2% (se cumule)
### Fonctionnalité 10
Le score augmente lorsque le joueur tue des ennemis.<br>
Le meilleur score est stocké dans un fichier texte. <br>
Il sera visible pendant la partie dans un coin de l’écran et nous pourrons voir le meilleur score réalisé sur le jeu.
## Elements du jeu
### Sprite :
- Personnage (8 directions)
- Slime bleu
- Slime tireur rouge
- Slime boss noir
- Des arbres pour limiter le terrain
- Un tire (du personnage)
- Pomme
- Champignon
- Huile

Tous les sprites seront réalisés par nos soins.
### Fond d'écran :
- Un fond d’accueil avec une forêt
- un fond vert qui sera de l’herbe durant la partie

### Effets sonores et musique :
- musique d’action en fond durant la partie
- bruitage lors d’un tire du personnage
- bruitage si un dégât est infligé à un ennemi
- bruitage si le personnage est touché par un ennemi
- un son si le joueur perd la partie
- un son s’il gagne

## Mécanismes du jeu
Les ennemis arrivent par vague depuis la forêt, autour du joueur, de plus en plus nombreux et avec des niveaux de difficulté progressifs. (Chaque round ajoute un ennemie, et tous les 3 un level) <br>
Dans les options, le joueur peut changer le volume du jeu et recommencer la partie (s’il est dans les menu depuis la partie). <br>
Il peut également voir son meilleur score.<br>
Un score est affiché.<br>
Des items apparaissent aléatoirement dans le jeu après avoir tué un ennemi avec un pourcentage de chance.<br>
## Développement
Le développement du projet sera réalisé en suivant les étapes suivantes :

### Conception et planification
#### Jour 1 (05/09/2023) :
- Création des sprites (Alexandre)
- Recherche de musique et bruitages (Yoann)
#### Jour 2 (12/09/2023) :
- Menu (Alexandre)
- Mouvement du personnage, tir du personnage, début ennemies
#### Jour 3 (19/09/2023) :
- Item, décor (Alexandre)
- Ennemies (Yoann)
#### Jour 4 (26/09/2023) :
- Ennemies (Alexandre)
- Ennemis suit le personnage (Yoann)
#### Jour 5 (03/10/2023) :
- Réglage de bug, d’éventuel retard, documentation technique, dernier bruitages
#### Jour 6 (10/10/2023) :
- Création de la présentation PowerPoint
## Tests et débogage
Les tests seront effectués à la fin de l’implémentation d’une tâche par l’autre personne qui la validera.

Les fonctionnalités déjà terminées seront re testées lorsqu' une nouvelle fonctionnalité aura été implémentée pour vérifier si la nouveauté n’a pas causé de problème à ce qui a déjà été fait. 
