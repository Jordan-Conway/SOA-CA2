async function vote(choiceId: string, questionId: string): Promise<Boolean> {
    throw new NotImplementedError("vote() is not implemented")
    return true
}

async function getQuestion(): Promise<Data> {
    throw new NotImplementedError("getQuestion() is not implemented")
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
