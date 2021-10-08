namespace Models.DTOs.Requests
{
    ////Не лучшее решения, связывать Response,Request, (связанность DTO ведёт к хаусу)
    //но проект не большой и поэтому сделал именно так
    //решением обычно является создания 3 вид моделей, и не наследование а агригация
    public class AddUserRequest : SignInRequest
    {
    }
}