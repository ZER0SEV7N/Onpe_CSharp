document.addEventListener('DOMContentLoaded', () => {

    //Cargar los elementos al DOM
    const cboAmbito = document.getElementById('cboAmbito');
    const cboDepartamento = document.getElementById('cboDepartamento');
    const cboProvincia = document.getElementById('cboProvincia');
    const cboDistrito = document.getElementById('cboDistrito');
    const cboLocal = document.getElementById('cboLocal');

    const contenedorMesas = document.getElementById('contenedorMesas');
    const contenedorActaDetalle = document.getElementById('contenedorActaDetalle');
    const grillaMesas = document.getElementById('grillaMesas');
    const htmlActa = document.getElementById('htmlActa');

    //Funcion para eliminar y reiniciar combos
    const limpiarCombo = (...combos) => {
        combos.forEach(combo => {
            combo.innerHTML = '<option value="">-- Seleccione --</option>';
        });
    };

    //Funcion para cargar los ambitos
    async function cargarAmbitos() {
        const ambito = cboAmbito.value;

        //Cambiar la etiqueta
        if (ambito === 'Nacional') {
            document.getElementById('lblDepartamento').textContent = 'Departamento:';
            document.getElementById('lblProvincia').textContent = 'Provincia:';
            document.getElementById('lblDistrito').textContent = 'Distrito:';
        } else {
            document.getElementById('lblDepartamento').textContent = 'Continente:';
            document.getElementById('lblProvincia').textContent = 'País:';
            document.getElementById('lblDistrito').textContent = 'Ciudad:';
        }

        limpiarCombo(cboDepartamento, cboProvincia, cboDistrito, cboLocal);
        contenedorMesas.style.display = 'none';
        contenedorActaDetalle.style.display = 'none';

        try {
            const res = await fetch(`${UrlsActa.departamentos}?ambito=${ambito}`);
            const data = await res.json();

            data.forEach(item => {
                cboDepartamento.add(new Option(item.text, item.id));
            });
        } catch (error) {
            console.error('Error al cargar los departamentos:', error);
        }
    }

    //Inicializar al cargar y asignar evento de cambio
    cargarAmbitos();
    cboAmbito.addEventListener('change', cargarAmbitos);

    //Cargar provincias
    cboDepartamento.addEventListener('change', async function () {
        const id = this.value;
        limpiarCombo(cboProvincia, cboDistrito, cboLocal);
        contenedorMesas.style.display = 'none';
        contenedorActaDetalle.style.display = 'none';

        if (id) {
            try {
                const res = await fetch(`${UrlsActa.provincias}?id=${id}`);
                const data = await res.json();
                data.forEach(item => cboProvincia.add(new Option(item.text, item.id)));
            } catch (error) {
                console.error('Error al cargar las provincias:', error);
            }
        }
    });

    //Cargar Distritos
    cboProvincia.addEventListener('change', async function () {
        const id = this.value;
        limpiarCombo(cboDistrito, cboLocal);
        contenedorMesas.style.display = 'none';
        contenedorActaDetalle.style.display = 'none';

        if (id) {
            try {
                const res = await fetch(`${UrlsActa.distritos}?id=${id}`);
                const data = await res.json();
                data.forEach(item => cboDistrito.add(new Option(item.text, item.id)));
            } catch (error) {
                console.error('Error al cargar los distritos:', error);
            }
        }
    });

    //Cargar Locales
    cboDistrito.addEventListener('change', async function () {
        const id = this.value;
        limpiarCombo(cboLocal);
        contenedorMesas.style.display = 'none';
        contenedorActaDetalle.style.display = 'none';

        if (id) {
            try {
                const response = await fetch(`${UrlsActa.locales}?id=${id}`);
                const data = await response.json();
                data.forEach(item => cboLocal.add(new Option(item.text, item.id)));
            } catch (error) { console.error(error); }
        }
    });

    //Cargar Mesas
    cboLocal.addEventListener('change', async function () {
        const id = this.value;
        grillaMesas.innerHTML = '';
        contenedorActaDetalle.style.display = 'none';

        if (id) {
            try {
                const response = await fetch(`${UrlsActa.mesas}?id=${id}`);
                const data = await response.json();

                data.forEach(item => {
                    const link = document.createElement('a');
                    link.textContent = item.id;
                    link.href = 'javascript:void(0)';
                    link.className = 'btn-mesa';
                    link.dataset.id = item.id;
                    link.style.cssText = 'display: inline-block; width:19%; text-align:center; padding:10px 0; color:#337ab7; font-weight:bold; text-decoration:none;';

                    link.addEventListener('mouseenter', () => link.style.backgroundColor = '#c0c0c0');
                    link.addEventListener('mouseleave', () => link.style.backgroundColor = 'transparent');

                    grillaMesas.appendChild(link);
                });

                contenedorMesas.style.display = 'block';
            } catch (error) {
                console.error('Error al cargar las mesas:', error);
            }
        } else
            contenedorMesas.style.display = 'none';
    });

    //Delegacion de eventos para cargar el Detalle del Acta
    grillaMesas.addEventListener('click', async function (e) {
        //Verificar si el clic fue en un elemento con clase 'btn-mesa'
        if (e.target && e.target.classList.contains('btn-mesa')) {
            e.preventDefault();

            const idMesa = e.target.dataset.id;

            try {
                const res = await fetch(`${UrlsActa.detalle}?id=${idMesa}`);
                const html = await res.text();

                contenedorMesas.style.display = 'none';
                htmlActa.innerHTML = html;
                contenedorActaDetalle.style.display = 'block';
            } catch (error) {
                alert("Error cargando el acta. Verifique que el método ObtenerDetalle exista en ActaController.");
                console.error(error);
            }
        }
    });

    //Regresar a la grilla de mesas
    document.getElementById('btnRegresar').addEventListener('click', () => {
        contenedorActaDetalle.style.display = 'none';
        contenedorMesas.style.display = 'block';
    });
});