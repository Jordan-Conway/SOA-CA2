async function getPokemon(dexNum) {
    const url = `https://pokeapi.co/api/v2/pokemon/${dexNum}/`
    const res = await fetch(url);
    const json = await res.json();
    return json.name;
}

function makeUrl(dexNum) {
    let paddedDexNum = "";
    if (dexNum < 10) {
        paddedDexNum = "00" + dexNum;
    }
    if (dexNum >= 10 && dexNum < 100) {
        paddedDexNum = "0" + dexNum;
    }
    if (dexNum >= 100 && dexNum <= 1024) {
        paddedDexNum = "" + dexNum;
    }
    return `https://www.pokemon.com/static-assets/content-assets/cms2/img/pokedex/full/${paddedDexNum}.png`
}

async function storePokemon(dexNum, name, image) {
    process.env["NODE_TLS_REJECT_UNAUTHORIZED"] = 0;
    const url = 'https://localhost:7015/api/Pokemon';
    const res = await fetch(url, {
        method: "POST",
        body: JSON.stringify({ id: dexNum, name: name, imageUrl: image }),
        headers: {
            "Content-Type": "application/json",
            "accept": "text/plain"
        }
    })
    if (res.status != 201) {
        console.log(res.status);
        console.log(res.statusText);
    }
}

async function main() {
    for (let i = 1; i <= 1024; i++) {
        const name = await getPokemon(i);
        const image = makeUrl(i);
        storePokemon(i, name, image);
    }
}

main();
