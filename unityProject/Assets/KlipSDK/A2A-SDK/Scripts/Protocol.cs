public class KlipProtocol {

    // KakaoTalk 관련
    public const string KAKAO_PACKAGE = "com.kakao.talk";
    public const int KAKAO_KLIP_SUPPORT_VERSION = 1908750;

    // Klip Client 관련
    public const string KAKAO_KLIP_LINK = "/?target=/a2a?request_key=";
    public const string INSTALL_REFERRER = "klipwallet_unity";

    // Klip Server 관련
    public const string SCHEME = "https";
    public const string API_VERSION = "/v2";

    public const string API_PREPARE_METHOD = "POST";
    public const string API_PREPARE_URL = "/a2a/prepare";
    public const string API_RESULT_METHOD = "GET";
    public const string API_RESULT_URL = "/a2a/result";
    public const string API_GET_CARD_METHOD = "GET";
    public const string API_GET_CARD_URL = "/a2a/cards";

    // Klip Server Request Field
    public const string CONTRACT = "contract";
    public const string TO = "to";
    public const string FROM = "from";
    public const string CARD_ID = "card_id";
    public const string VALUE = "value";
    public const string ABI = "abi";
    public const string PARAMS = "params";
    public const string AMOUNT = "amount";

    public const string BAPP = "bapp";
    public const string TRANSACTION = "transaction";
    public const string TYPE = "type";

    public const string BAPP_NAME = "name";
    public const string BAPP_CALLBACK = "callback";
    public const string BAPP_SUCCESS_URL = "success";
    public const string BAPP_FAIL_URL = "fail";

    // Klip Server Response Field
    public const string NAME = "name";
    public const string SYMBOL_IMG = "symbol_img";
    public const string CARDS = "cards";
    public const string NEXT_CURSOR = "next_cursor";

    public const string CREATED_AT = "created_at";
    public const string UPDATED_AT = "updated_at";
    public const string OWNER = "owner";
    public const string SENDER = "sender";
    public const string CARD_URI = "card_uri";
    public const string TRANSACTION_HASH = "transaction_hash";

    public const string REQUEST_KEY = "request_key";
    public const string EXPIRATION_TIME = "expiration_time";
    public const string STATUS = "status";
    public const string RESULT = "result";
    public const string ERROR = "error";

    public const string TX_HASH = "tx_hash";
    public const string KLAYTN_ADDRESS = "klaytn_address";

    public const string CODE = "code";
    public const string MESSAGE = "message";

    public const string ERR = "err";

    // Klip 세팅
    public const string PHASE = "com.klipwallet.app2app.Phase";

    public static string apiAuthority() {
        return "a2a-api.klipwallet.com";
    }

    public static string linkAuthority() {
        return "klipwallet.com";
    }
}