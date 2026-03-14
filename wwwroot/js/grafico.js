//Cargar Api de google Charts
google.charts.load('current', { 'packages': ['corechart'] });
google.charts.setOnLoadCallback(drawChart);

//Función para dibujar el gráfico
function drawChart() {
    var contenedor = document.getElementById('grafico');
    if (!contenedor) return;

    //Leer los datos del atributo data-datos
    var pctPA = parseFloat(contenedor.getAttribute('data-participacion')) || 0;
    var pctAU = parseFloat(contenedor.getAttribute('data-ausentismo')) || 0;

    //Crear la tabla datos
    var data = google.visualization.arrayToDataTable([
        ['Estado', 'Porcentaje'],
        ['Participación', pctPA],
        ['Ausentismo', pctAU]
    ]);

    //Opciones del grafico
    var opciones = {
        is3D: true,
        colors: ['#1c2742', '#a9a9a9'],
        legend: { position: 'bottom' },
        chartArea: { width: '90%', height: '80%' },
        pieSliceText: 'percentage'
    };

    //Dibujar el gráfico
    var chart = new google.visualization.PieChart(contenedor);
    chart.draw(data, opciones);
}