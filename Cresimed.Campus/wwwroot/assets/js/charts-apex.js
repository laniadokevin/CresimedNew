/**
 * Charts Apex
 */

'use strict';

(function () {
  let cardColor, headingColor, labelColor, borderColor, legendColor, radialTrackColor;

  if (isDarkStyle) {
    cardColor = config.colors_dark.cardColor;
    headingColor = config.colors_dark.headingColor;
    labelColor = config.colors_dark.textMuted;
    legendColor = config.colors_dark.bodyColor;
    borderColor = config.colors_dark.borderColor;
    radialTrackColor = '#36435C';
  } else {
    cardColor = config.colors.cardColor;
    headingColor = config.colors.headingColor;
    labelColor = config.colors.textMuted;
    legendColor = config.colors.bodyColor;
    borderColor = config.colors.borderColor;
    radialTrackColor = config.colors_label.secondary;
  }

  // Color constant
  const chartColors = {
    column: {
      series1: '#826af9',
      series2: '#d2b0ff',
      bg: '#f8d3ff'
    },
    donut: {
      series1: '#fee802',
      series2: '#3fd0bd',
      series3: '#826bf8',
      series4: '#2b9bf4'
    },
    area: {
      series1: '#29dac7',
      series2: '#60f2ca',
      series3: '#a5f8cd'
    }
  };


  // Radar Chart
  // --------------------------------------------------------------------
  const radarChartEl = document.querySelector('#radarChart'),
    radarChartConfig = {
      chart: {
        height: 350,
        fontFamily: 'IBM Plex Sans',
        type: 'radar',
        toolbar: {
          show: false
        },
        dropShadow: {
          enabled: false,
          blur: 8,
          left: 1,
          top: 1,
          opacity: 0.2
        }
      },
      legend: {
        show: true,
        position: 'bottom',
        labels: {
          colors: legendColor,
          useSeriesColors: false
        }
      },
      plotOptions: {
        radar: {
          polygons: {
            strokeColors: borderColor,
            connectorColors: borderColor
          }
        }
      },
      yaxis: {
        show: false
      },
      series: [
        {
          name: 'Yo',
          data: [41, 64, 81, 60, 42, 42, 33, 23]
        },
        {
          name: 'Promedio',
          data: [99, 99, 99, 25, 58, 63, 76, 43]
        }
      ],
      colors: [chartColors.donut.series1, chartColors.donut.series3],
      xaxis: {
        categories: ['Battery', 'Brand', 'Camera', 'Memory', 'Storage', 'Display', 'OS', 'Price'],
        labels: {
          show: true,
          style: {
            colors: [labelColor, labelColor, labelColor, labelColor, labelColor, labelColor, labelColor, labelColor],
            fontSize: '13px',
            fontFamily: 'IBM Plex Sans'
          }
        }
      },
      fill: {
        opacity: [1, 0.8]
      },
      stroke: {
        show: false,
        width: 0
      },
      markers: {
        size: 0
      },
      grid: {
        show: false,
        padding: {
          top: -20,
          bottom: -20
        }
      }
    };
  if (typeof radarChartEl !== undefined && radarChartEl !== null) {
    const radarChart = new ApexCharts(radarChartEl, radarChartConfig);
    radarChart.render();
  }

})();
