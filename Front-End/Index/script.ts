let apiUrl = "https://localhost:7015/api/"

let loginButton: Element
let loginModal: HTMLDialogElement
let createQuestionForm: HTMLFormElement

let imageElement1: Element
let imageElement2: Element
let image1: Picture
let image2: Picture

document.addEventListener('DOMContentLoaded', function() {
    load()
    loadImages()
 }, false);

function load()
{
    loginButton = document.getElementById("loginButton")
    loginModal = document.getElementById("loginDialog") as HTMLDialogElement
    createQuestionForm = document.getElementById("questionForm") as HTMLFormElement

    imageElement1 = document.getElementById("picture1")
    imageElement2 = document.getElementById("picture2")

    loginModal.close()

    loginButton.addEventListener("click", () => {
        loginModal.showModal()
    })

    createQuestionForm.addEventListener("submit", (e) => {
        e.preventDefault()
        let textInput = document.getElementById("questionText") as HTMLInputElement
        addQuestion(textInput.value)
    })
}

async function loadImages()
{
    let pokemon1 = await getPokemon()
    let pokemon2 = await getPokemon()
    image1 = pokemon1
    image2 = pokemon2

    imageElement1.setAttribute("src", image1.imageUrl)
    imageElement2.setAttribute("src", image2.imageUrl)
}

async function vote(choiceId: string, questionId: string): Promise<Boolean> {
    throw new NotImplementedError("vote() is not implemented")
    return true
}

//https://stackoverflow.com/a/60166572
async function getPokemon(): Promise<Picture> {
    let url = apiUrl + "Pokemon/random"
    
    return fetch(url)
        .then((res) => res.json())
        .then((res) => {return res as Promise<Picture>})
}

async function getQuestion(): Promise<Question> {
    throw new NotImplementedError("getQuestion() is not implemented")
}

function voteDebug(): void {
    let optionPage = document.getElementById("optionsPage")
    let resultPage = document.getElementById("resultPage")

    if(optionPage == undefined || resultPage == undefined)
    {
        console.log("Could not find page")
        return
    }

    optionPage.setAttribute('style', 'display:none')
    resultPage.setAttribute('style', '')
}

function goBackDebug(): void {
    let optionPage = document.getElementById("optionsPage")
    let resultPage = document.getElementById("resultPage")
    let questionPage = document.getElementById("questionPage")
    let createQuestionPage = document.getElementById("createQuestionPage")

    optionPage.setAttribute('style', '')
    resultPage.setAttribute('style', 'display:none')
    questionPage.setAttribute('style', '')
    createQuestionPage.setAttribute('style', 'display:none')
}

function createQuestionDebug(): void {
    let questionPage = document.getElementById("questionPage")
    let createQuestionPage = document.getElementById("createQuestionPage")

    questionPage.setAttribute("style", "display:none")
    createQuestionPage.setAttribute("style", "display:''")
}

function toggleLogin(): void {
    
}

function addQuestion(questionText: string)
{
    throw new NotImplementedError("addQuestion() is not implemented")
}

function toggleRegister(): void {
    let loginForm = document.getElementById("loginForm")
    let registerForm = document.getElementById("registerForm")

    if(loginForm.getAttribute('style') === "display:''")
    {
        loginForm.setAttribute('style', 'display:none')
        registerForm.setAttribute('style', '')
    }
    else
    {
        registerForm.setAttribute('style', 'display:none')
        loginForm.setAttribute('style', '')
    }
}

interface Picture {
    id: string,
    name: string,
    imageUrl: string,
}

interface Question {
    id: string,
    text: string
}

interface Data{
    picture1: Picture,
    picture2: Picture,
    question: Question
}

class NotImplementedError extends Error
{
    constructor(message)
    {
        super(message)
        this.name = "NotImplementedError"
    }
}
