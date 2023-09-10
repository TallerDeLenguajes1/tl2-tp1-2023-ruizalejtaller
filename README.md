# Taller de Lenguajes II
## Práctico 1

_Ruiz, Ramón Alejandro_

## Punto 2
### ¿Cuál de estas relaciones considera que se realiza por composición y cuál por agregación?
- Por composición: Pedido - Cliente
- Por agregación: Cadetería - Cadetes, Cadete - Pedido

### ¿Qué métodos considera que debería tener la clase Cadetería y la clase Cadete?
- Algunos de los métodos de Cadetería serían:
- DarAlta, AsignarACadete, CambiarEstado, MostrarPendientes, ReasignarCadetes, Informe

- Y en la clase Cadete:
- AgregarPedido, MostrarPedido, QuitarPedido, JornalACobrar, PedidosEntregados

### Teniendo en cuenta los principios de abstracción y ocultamiento, qué atributos, propiedades y métodos deberían ser públicos y cuáles privados.
-En todas las clases tome la descision de hacer privados los atributos y acceder a ellos mediante propiedades, así como los métodos todos públicos

### ¿Cómo diseñaría los constructores de cada una de las clases?
- En Cadeteria `public Cadeteria(string nombre, string telefono)`
- En Cadete:  `public Cadete (int Id, string Nombre, string Direccion, string Telefono)`
- En Pedido: `public Pedido(int nro, string obs, Cliente cliente, Estados estado)`
- Y en Cliente: `public Cliente(string nombre, string direccion, string telefono, string datosreferenciadireccion)`
