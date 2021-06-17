Versión de subida: 1

**Video**

Podéis ver un vídeo explicativo y demostrativo del juego en el siguiente enlace: https://www.youtube.com/watch?v=AsUbkS_zzmw&ab_channel=JoanFreixas

**JUEGO**

Un juego en 3ª persona shooter que trata de un apocalypsis zombie. En realidad, la historia nos dice que estamos en una simulación para prepararnos para un apocalipsis real. Habrá dos misiones: sobrevivir durante 5 minutos a un asalto zombie en la zona inicial y posteriormente destruir 12 cristales. Cuando logramos los dos objetivos se habrá ganado el nivel.
La jugabilidad ofrece una visión en vista de pájaro del jugador que se desplaza con WASD y con el cursor apunta y dispara. Además, los vehículos pueden ser controlados, cuando el jugador se acerca a uno de ellos y pulsa F, se monta al coche y también con WASD se controla su movimiento.
Por el mapa hay humanos deambulando. Estos huyen de los zombies en cuanto se acerca uno de ellos y cuando se alejan vuelven a su ruta inicial. Si el jugador disapara a un humano, este se convertirá en zombie (siguiendo la lógica de The Walking Dead, que aunque no te hayan mordido, te conviertes en zombie tras morir).

**DESARROLLO**

El juego tiene una escena principal que es la del nivel, donde se produce todo el gameplay. Además también contiene las escenas de los menús: menú principal y menú de victoria.

_Prefabs_

-	Player: 
    -	PlayerMovement.cs: Maneja el desplazamiento cuando éste está vivo. El desplazamiento es muy básico, únicamente son los ejes sin manipular rotaciones. Pero el personaje rota mirando el cursor, esto se ha hecho con un raycast desde la cámara al cursor y que el componente transform rote según el raycast. Con el TriggerStay, si se detecta el coche y se presiona F, activa los códigos del coche.
    -	PlayerHealth.cs: Contiene un método para cuando el jugador recibe daño. Este es invocado por los códigos del zombie y resta vida. También activa el marco rojos cuando es impactado.
    -	ShootingController.cs: En clicar, instancia una y maneja la munición.
    -	Se le ha incorporado el componente navMeshObstacle para que los zombies y humanos no colisionen con él.
-	Car:
    -	 CarEnterExit.cs: cuando el jugador pasa a modo conducción, desactiva los componentes visuales y transforma al jugador en hijo del coche. Si el jugador pasa a modo caminar vuelve a activar los componentes visuales y lo vuelve a objeto independiente.
    -	CarMovement.cs: El coche consta de una esfera, que es la que se desplaza. Esta únicamente tiene los controles básicos WASD, con el que el eje vertical indica aceleración y el eje horizontal la rotación (no desplazamiento). La rueda se desplaza, y el modelo del coche copia la posición y rotación Y de esta.
    -	También tiene el navMeshObstacle.
-	Zombie:
    -	El componente principal es el NavMeshAgent, que permitirá el desplazamiento inteligente por el mapa.
    -	El zombie tiene la misma estructura que en la PEC2. Un script general para gestionar la inteligencia artificial y manejar los estados mientras el zombie esté vivo (paradójico). Tiene 3 estados: wander, chase y attack. Todos implementan una interfaz para facilitar su desarrollo. Para detectar al jugador tiene un sphere collider en modo trigger.
    -	WanderState: El zombie patrulla por un vector de puntos del mapa. Cuando alcanza un punto, el script selecciona aleatoriamente otro del vector y se dirige a este. Si el jugador activa el collider, se actualiza el estado a Chase.
    -	ChaseState: Persigue al jugador, ya no hay vuelta atrás. Si el zombie te detecta, te perseguirá hasta que te alcance o muera. Si el jugador está a una distancia determinada, éste procederá al estado Attack.
    -	AttackState: quieto, activa la animación ataque. Cuando la mano se encuentra en un punto determinado, la animación invoca un evento que comprueba si ha tocado al jugador o no. Si lo ha tocado, llamará al método TakeDamage del jugador (PlayerHealth) y le restará vida. Si el jugador se aleja, volverá a ChaseState.
    -	ZombieHealth: Controla la vida del zombie, funciona del mismo modo que para el jugador. Un método para reducir la vida, se llama cuando una bala lo alcanza.
-	ZombieBonus:
    -	Los cógidos son iguales que en el zombie anterior, pero con la diferencia de que el zombie no patrulla, directamente persigue al jugador.
-	Human:
    -	El humano se desplaza de la misma manera que el zombie patrulla, pero con un orden de puntos no aleatorios.
    -	Lleva un sphere collider en modo trigger que en cuanto detecta un zombie corre en dirección opuesta al zombie. Cuando se ha alejado una distancia determinada, vuelve a su ruta inicial.
    -	Si el jugador dispara al humano, éste muere e instancia un zombie. El zombie tomará los puntos de ruta del humano. 
-	Otros:
    -	Cajas de munición y packs de vida: Llevan colliders que en cuanto el jugador los activa, llaman a sus métodos para añadir munición o vida.
    -	Cámara:  el prefab es un objeto que sigue la posición del jugador y que como hijo contiene la cámara, en una ubicación más elevada y con ángulo enfocando al personaje.
    - Edificios transparentes: todos los edificios tienen un cubo en modo trigger que en cuanto el jugador lo activa, se cambia el material de estos por uno transparente para que el jugador pueda ver dónde se encuentra su personaje cuando está detrás de los edificios. EN cuanto sale del trigger, vuelve a recuperar el material anterior.

_Nivel y misiones_

El nivel se trata de 3 zonas de edificios, dos de ellas infestadas de zombies y la otra con civiles. Además de una zona abandonada con muchos contenedores tóxicos. En esta zona abandonada empieza la primera misión, aguantar 5 minutos. Cuando el tiempo pasa el texto de la misión se colorea verde.
Durante este asalto de 5 minutos, el mapa genera 3 zombies cada 7 segundos en ubicaciones aleatorias. A medida que pasa el tiempo, el intervalo entre spawn de zombies se va disminuyendo, haciendo que cada vez haya más.
El jugador puede huir de la zona, pero eso no quita que tenga que una horda gigante de zombies persiguiéndole y que hasta que no pasen los 5 minutos no habrá superado la misión.
Las zonas con edificios tienen cristales que deberán ser destruidos a disparos del jugador. En cuanto se destruyan todos, el texto de la misión se colorea verde también.
Para controlar el seguimiento de las misiones, hay un script llamado GoalManager.cs que lleva un conteo de las misiones, cuando una misión es superada suma 1. Cuando el total de misiones superadas alcanza el total de misiones, el  script cargará la escena de victoria.

**Features que debido al tiempo no he podido añadir**

-	El 100% de la animación del jugador.
-	El audio de los vehículos, se investigó una manera de reproducir el sonido a corde con la velocidad, con mixers y mezlcando 3 audios diferentes, pero el tiempo no permitió desarrollarlo bien y quedó descartado.
-	Diferentes armas o boosts. Se hubieran desarrollado de la misma manera que en la PEC2.
-	Las colisiones entre vehículos y personajes. Por algún motivo no detectaba nunca los zombies ni humanos, el tiempo no  ha permitido encontrar la solución.
-   Aaque cuerpo a cuerpo: el desarrollo es muy sencillo. Al pusar la tecla, triggea la animación de golpear y funciona de la misma manera que el ataque del zombie. Activa un checkSphere que si detecta un zombie, llama a su método de dañar.

**Posibles mejoras que pueden ser interesantes**

-	Un minimapa en la HUD.
-	Menú de pausa.
-	Más misiones y un mapa más grande.
-	Personajes interactuables. Que te proporcionen misiones.
-	El movimiento del zombie patrullando inicialmente estaba pensado en un movimiento de steering. Se creó una escena para probar los prefabs, y en esta escena sí actuaba bien el comportamiento steering, pero en la escena del juego no respondía y se dejó con el vector de posiciones.
