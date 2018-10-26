# Terminal-Drive

+5 escenas (Estetica similar a The Last Night):
	-oficina
	-ciudad
	-tienda
	-metro (pantalla carga)
	-zona mision

+Gameplay zonas de no mision (ciudad, oficina, tienda)
	-ciudad (moverte, entrar edificios)
	-oficina (moverte, usar terminal misiones)
	-tienda (moverte, hablar tendero para comprar)

+Gameplay zona de mision (sigilo, accion, hackeo):
	-sigilo: te detectan al disparar y correr, no te detectan al usar invisibilidad o ir agachado
	-accion: disparar, melee, implantes ofensivos
	-hackeo: opcional	

+Enemigos:
	-humanos con estilos faccion (estaticos con posibilidad de movimiento(como hotline miami), disparan hacia donde estes libremente)
	-opcional: perros.

+Elementos graficos:
	-fondos de escenario (a debatir)
	-puertas (2 sprites - abierto y cerrado)
	-paredes (1 - posibles diferentes diseños)
	-ventanas (2 sprites - intacta y rota)
	-suelos (1 - posibles diferentes diseños)
	-conductos (1 - tileset)
	-luces (opcional)

+Moviento:
	-correr (defecto)
	-agacharte
	-disparar libremente (apuntar con raton)
	-saltar

+Interfaz:
	+barra de vida:
		-se empieza la mision con 100% de vida	
		-se regenera con consumibles por defecto (automaticamente con implante)
		-se reduce al recibir daño (enemigos, caidas)
	+barra de energia: 
		-se gasta con el uso de las habilidades de los implantes
		-se empieza la mision con 100% de energia
		-si se gasta entera o hasta menos de un 10% solo se puede recargar hasta el 10%
		-posibilidad de recargar hasta el 100% con consumibles
 
+Objetos maximos inventario personaje:
	-armas a distancia (2 armas pequeñas o 1 grande y 1 pequeña)
	-melee (arma melee o puños)
	-1 consumible (se puede soltar o si no se tiene por soltarlo o por usarlo se pueden recoger del escenario)

+Implantes: 
	-dan habilidades extra al personaje
	-x implantes totales en el juego
	-5 implantes por partida aleatorios de los totales en el juego (disponibles tienda de implantes)
	-al tener un implante instalado en el mercado aparece la posibilidad de mejorarlo

+Protesis:
	+posibles protesis:
		-torso: mejora resistencia (ejemplo: si se tiene 100 de vida --> con implante 200(morir)/300(operacion))
		-brazos: mejoran combate (mayor daño melee, mayor cadencia disparo)
		-piernas: mejoran movimiento (mayor salto y mayor velocidad de movimiento)
	+solo posible obtenerlas:
		-al morir (parte aleatoria --> +stats base)
		-al operarse (seleccion parte del cuerpo a sustituir --> +stats base y +bonus extra stats)
	-cada protesis implica estar mas cerca del final de la partida (no importa si es al morir o al operarse)
	-idea a comentar en grupo: las protesis obtenidas por operacion a parte de "+bonus extra stats" se añade un implante a elegir no disponible 		en la tienda de implantes de los implantes totales del juego
