let loginButton: Element
let loginModal: HTMLDialogElement

let image1: Element
let image2: Element
let image1Url: string = "../Images/1.jpeg"
let image2Url: string = "../Images/2.jpeg"

document.addEventListener('DOMContentLoaded', function() {
    load()
    loadImages()
 }, false);

function load()
{
    loginButton = document.getElementById("loginButton")
    loginModal = document.getElementById("loginDialog") as HTMLDialogElement

    image1 = document.getElementById("picture1")
    image2 = document.getElementById("picture2")

    loginModal.close()

    loginButton.addEventListener("click", () => {
        loginModal.showModal()
    })
}

function loadImages()
{
    image1.setAttribute("src", image1Url)
    image2.setAttribute("src", image2Url)
}

async function vote(choiceId: string, questionId: string): Promise<Boolean> {
    throw new NotImplementedError("vote() is not implemented")
    return true
}

async function getQuestion(): Promise<Data> {
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

interface Picture {
    id: string,
    name: string,
    url: string,
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
