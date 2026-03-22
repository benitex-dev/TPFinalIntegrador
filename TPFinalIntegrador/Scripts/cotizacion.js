//endpoints de dolarapi.com
const endpointUSD = "https://dolarapi.com/v1/dolares/oficial";
const endpointEUR = "https://dolarapi.com/v1/cotizaciones/eur";
const endpointBRL = "https://dolarapi.com/v1/cotizaciones/brl";

function getEndpointByMoneda(moneda) {
    switch (moneda) {
        case "USD":
            return endpointUSD;
        case "EUR":
            return endpointEUR;
        case "BRL":
            return endpointBRL;
        default:
            return null;
    }
}

async function getDataFromAPI(url) {
    try {
        const response = await fetch(url);
        if (!response.ok) {
            throw new Error('Error al consultar la API');
        }
        return await response.json();
    } catch (error) {
        console.error(error);
        throw error;
    }
}

async function obtenerCotizacion(moneda) {
   
    const url = getEndpointByMoneda(moneda);
    let data;

        //como respuesta obtenemos un objeto con varias propiedades, entre ellas "venta" que es la que nos interesa para convertir a pesos.
        data = await getDataFromAPI(url);
        console.log("Data que obtenemos de la API: ", data);
        return data.venta;
       
}