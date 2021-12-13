package connection;

import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.TimeZone;

public class getTime {
	public String GetTime()
	{
		SimpleDateFormat sd = new SimpleDateFormat("h:mma");
        Date date = new Date();
        sd.setTimeZone(TimeZone.getTimeZone("IST"));
        System.out.println(sd.format(date));
        return sd.format(date);
	}

	public static void main(String[] args) {
		 

	}

}
