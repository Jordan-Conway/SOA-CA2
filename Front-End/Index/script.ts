let apiUrl = "https://localhost:7015/api/"

let loginButton: Element
let loginModal: HTMLDialogElement
let createQuestionForm: HTMLFormElement
let questionText: HTMLParagraphElement

let imageElement1: Element
let imageElement2: Element
let image1: Picture
let image2: Picture

let voteButton1: Element
let voteButton2: Element

document.addEventListener('DOMContentLoaded', function() {
    load()
    loadImages()
    loadQuestion()
 }, false);

function load()
{
    loginButton = document.getElementById("loginButton")
    loginModal = document.getElementById("loginDialog") as HTMLDialogElement
    createQuestionForm = document.getElementById("questionForm") as HTMLFormElement
    questionText = document.getElementById("questionText") as HTMLParagraphElement

    imageElement1 = document.getElementById("picture1")
    imageElement2 = document.getElementById("picture2")

    voteButton1 = document.getElementById("vote1")
    voteButton2 = document.getElementById("vote2")

    loginModal.close()

    loginButton.addEventListener("click", () => {
        loginModal.showModal()
    })

    createQuestionForm.addEventListener("submit", (e) => {
        e.preventDefault()
        let textInput = document.getElementById("questionText") as HTMLInputElement
        submitQuestion(textInput.value)
    })
}

async function loadImages()
{
    refeshImages()

    voteButton1.addEventListener('click', function(e) 
    {
        vote(image1.id, "1")
    })
    voteButton2.addEventListener('click', function(e) 
    {
        vote(image2.id, "1")
    })
}

async function loadQuestion() {
    let question = await getQuestion()
    console.log(question.question)
    questionText.innerText = question.question
}

async function refeshImages()
{
    let pokemon1 = await getPokemon()
    let pokemon2 = await getPokemon()
    image1 = pokemon1
    image2 = pokemon2

    imageElement1.setAttribute("src", image1.imageUrl)
    imageElement2.setAttribute("src", image2.imageUrl)
}

async function vote(choiceId: string, questionId: string): Promise<Boolean> {
    let url = apiUrl + "Vote/cast/"
    let questionIdNum: number = Number(questionId)

    if(Number.isNaN(questionIdNum))
    {
        console.error("Invalid questionId: " + questionId)
    }
    
    fetch(url, {
        method: "POST",
        headers:{
            'Content-Type': "application/json"
        },
        body: JSON.stringify({
                "PokemonId": choiceId,
                "QuestionId": questionIdNum,
        })
    })

    console.log("Voted for: " + choiceId)
    refeshImages()
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
    let url = apiUrl + "Question/random"
    return fetch(url)
        .then(res => res.json())
        .then(res => {return res})
}

async function submitQuestion(text: string): Promise<Boolean>
{
    let url = apiUrl + "Question"

    fetch(url, {
        method: "POST",
        headers:{
            'Content-Type': "application/json"
        },
        body: JSON.stringify({
            "Question": text,
            "CreatedBy": "TEST"
        })
    })

    return true
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
    question: string
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
